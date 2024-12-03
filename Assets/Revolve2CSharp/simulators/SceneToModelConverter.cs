using System;
using System.Collections.Generic;
using System.Linq;
using Mujoco;
using Revolve2.Simulation;
using Revolve2.Simulation.Mujoco; // Placeholder for your MuJoCo C# binding

public static class SceneToModelConverter
{
    public static unsafe (MujocoLib.mjModel_*, AbstractionToMujocoMapping) ConvertToModel(
        Scene scene,
        float simulationTimestep,
        bool castShadows,
        bool fastSim)
    {
        var mapping = new AbstractionToMujocoMapping();

        // Create root element
        var envMjcf = new MjcfRootElement
        {
            ModelName = "scene",
            CompilerAngle = "radian",
            Option = new MjcfOptions
            {
                Timestep = simulationTimestep,
                Integrator = "RK4",
                Gravity = new[] { 0.0f, 0.0f, -9.81f }
            }
        };

        envMjcf.WorldBody.AddLight(new MjcfLight
        {
            Position = new[] { 0.0f, 0.0f, 100.0f },
            Ambient = new[] { 0.5f, 0.5f, 0.5f },
            Directional = true,
            CastShadow = castShadows
        });

        envMjcf.Visual.Headlight.Active = 0;

        // Handle multi-body systems
        var conversions = scene.MultiBodySystems
            .Select((mbs, index) => ConvertMultiBodySystemToUrdf(mbs, $"mbs{index}"))
            .ToList();

        var allJointsAndNames = conversions.Select(c => c.JointsAndNames).ToList();
        var allRigidBodiesAndNames = conversions.Select(c => c.RigidBodiesAndNames).ToList();

        var heightmaps = new List<GeometryHeightmap>();

        // Add multi-body systems
        for (int mbsIndex = 0; mbsIndex < conversions.Count; mbsIndex++)
        {
            var conversion = conversions[mbsIndex];
            var urdfModel = new MjModel(conversion.UrdfXml);
            var attachmentFrame = envMjcf.AttachModel(urdfModel);

            var pose = scene.MultiBodySystems[mbsIndex].Pose;
            attachmentFrame.Position = pose.Position.ToArray();
            attachmentFrame.Quaternion = pose.Orientation.ToArray();

            if (!scene.MultiBodySystems[mbsIndex].IsStatic)
            {
                attachmentFrame.AddFreeJoint();
            }

            // Add actuation to joints
            AddJointActuators(conversion.JointsAndNames, urdfModel);

            // Add sensors
            AddSensors(conversion.RigidBodiesAndNames, mbsIndex, urdfModel, envMjcf);

            // Add plane geometries
            AddPlanes(conversion.PlaneGeometries, fastSim, envMjcf);

            // Add heightmaps
            var addedHeightmaps = AddHeightmaps(conversion.HeightmapGeometries, fastSim, envMjcf);
            heightmaps.AddRange(addedHeightmaps);

            // Set colors and materials
            SetColorsAndMaterials(conversion.GeomsAndNames, urdfModel, fastSim);
        }

        var xml = envMjcf.ToXmlString();
        var model = MjModel.FromXmlString(xml);

        // Set heightmap values
        SetHeightmapValues(heightmaps, model);

        return (model, mapping);
    }

    private static void AddJointActuators(
        List<(JointHinge, string)> jointsAndNames,
        MjModel model)
    {
        foreach (var (joint, name) in jointsAndNames)
        {
            var jointMjcf = model.FindJoint(name);
            jointMjcf.Armature = joint.Armature;

            model.AddActuator(new MjActuator
            {
                Type = "position",
                Gain = joint.PidGainP,
                Joint = jointMjcf,
                Name = $"actuator_position_{name}"
            });

            model.AddActuator(new MjActuator
            {
                Type = "velocity",
                Gain = joint.PidGainD,
                Joint = jointMjcf,
                Name = $"actuator_velocity_{name}"
            });
        }
    }

    private static void AddPlanes(
        List<GeometryPlane> planes,
        bool fastSim,
        MjcfRootElement envMjcf)
    {
        foreach (var plane in planes)
        {
            var materialName = $"plane_material_{Guid.NewGuid()}";
            var geom = new MjcfGeom
            {
                Type = "plane",
                Position = plane.Pose.Position.ToArray(),
                Quaternion = plane.Pose.Orientation.ToArray(),
                Size = new[] { plane.Size.X / 2.0f, plane.Size.Y / 2.0f, 1.0f }
            };

            if (fastSim)
            {
                geom.Rgba = plane.Texture.BaseColor.ToNormalizedRgbaArray();
            }
            else
            {
                geom.Material = materialName;
                envMjcf.AddMaterial(materialName, plane.Texture);
            }

            envMjcf.WorldBody.AddGeom(geom);
        }
    }
}
