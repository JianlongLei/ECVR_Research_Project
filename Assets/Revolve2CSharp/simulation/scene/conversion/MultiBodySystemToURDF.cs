using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Numerics;
using Revolve2.Geo;
using Revolve2.Joints;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Utility to convert a multi-body system into URDF format.
    /// </summary>
    public static class MultiBodySystemToURDF
    {
        /// <summary>
        /// Converts a multi-body system into a URDF representation.
        /// </summary>
        /// <param name="multiBodySystem">The multi-body system to convert.</param>
        /// <param name="name">The name to use in the URDF model.</param>
        /// <returns>A tuple containing the URDF string and associated data.</returns>
        public static (string urdf, List<GeometryPlane> planes, List<GeometryHeightmap> heightmaps,
            List<(JointHinge, string)> joints, List<(Geometry, string)> geometries, List<(RigidBody, string)> rigidBodies)
            Convert(MultiBodySystem multiBodySystem, string name)
        {
            if (!multiBodySystem.HasRoot())
                throw new InvalidOperationException("The multi-body system must have a root.");

            var converter = new URDFConverter();
            return converter.Build(multiBodySystem, name);
        }

        private class URDFConverter
        {
            private string _baseName;
            private MultiBodySystem _multiBodySystem;
            private readonly HashSet<Guid> _visitedRigidBodies = new();
            private readonly List<(JointHinge, string)> _jointsAndNames = new();
            private readonly List<(Geometry, string)> _geometriesAndNames = new();
            private readonly List<(RigidBody, string)> _rigidBodiesAndNames = new();
            private readonly List<GeometryPlane> _planes = new();
            private readonly List<GeometryHeightmap> _heightmaps = new();

            public (string urdf, List<GeometryPlane> planes, List<GeometryHeightmap> heightmaps,
                List<(JointHinge, string)> joints, List<(Geometry, string)> geometries, List<(RigidBody, string)> rigidBodies)
                Build(MultiBodySystem multiBodySystem, string name)
            {
                _multiBodySystem = multiBodySystem;
                _baseName = name;

                var rootElement = new XElement("robot", new XAttribute("name", name));

                foreach (var element in CreateLinksXmlElements(multiBodySystem.Root, multiBodySystem.Root.InitialPose, name, null))
                {
                    rootElement.Add(element);
                }

                var urdfString = new XDocument(rootElement).ToString();

                return (urdfString, _planes, _heightmaps, _jointsAndNames, _geometriesAndNames, _rigidBodiesAndNames);
            }

            private IEnumerable<XElement> CreateLinksXmlElements(RigidBody rigidBody, Pose linkPose, string rigidBodyName, RigidBody? parentRigidBody)
            {
                if (_visitedRigidBodies.Contains(rigidBody.UUID))
                    throw new InvalidOperationException("Multi-body system is cyclic.");

                _visitedRigidBodies.Add(rigidBody.UUID);
                var elements = new List<XElement>();

                var link = new XElement("link", new XAttribute("name", rigidBodyName));
                elements.Add(link);
                _rigidBodiesAndNames.Add((rigidBody, rigidBodyName));

                if (rigidBody.Mass > 0)
                {
                    var com = CalculateCenterOfMass(linkPose, rigidBody);
                    var inertia = rigidBody.InertiaTensor();
                    var inertial = new XElement("inertial",
                        new XElement("origin", new XAttribute("xyz", $"{com.Position.X} {com.Position.Y} {com.Position.Z}"),
                            new XAttribute("rpy", $"{com.Orientation.X} {com.Orientation.Y} {com.Orientation.Z}")),
                        new XElement("mass", new XAttribute("value", $"{rigidBody.Mass}")),
                        new XElement("inertia",
                            new XAttribute("ixx", $"{inertia.M11}"),
                            new XAttribute("iyy", $"{inertia.M22}"),
                            new XAttribute("izz", $"{inertia.M33}"),
                            new XAttribute("ixy", $"{inertia.M12}"),
                            new XAttribute("ixz", $"{inertia.M13}"),
                            new XAttribute("iyz", $"{inertia.M23}")));
                    link.Add(inertial);
                }

                foreach (var geometry in rigidBody.Geometries)
                {
                    AddGeometry(link, rigidBodyName, geometry, linkPose, rigidBody);
                }

                foreach (var joint in _multiBodySystem.GetJointsForRigidBody(rigidBody))
                {
                    if (parentRigidBody != null && (joint.RigidBody1 == parentRigidBody || joint.RigidBody2 == parentRigidBody))
                        continue;

                    if (joint is not JointHinge hingeJoint)
                        throw new InvalidOperationException("Only hinge joints are supported.");

                    AddJoint(elements, joint, hingeJoint, rigidBodyName, linkPose, rigidBody);
                }

                return elements;
            }

            private (Vector3 Position, Vector3 Orientation) CalculateCenterOfMass(Pose linkPose, RigidBody rigidBody)
            {
                var localCom = rigidBody.CenterOfMass() - linkPose.Position;
                var orientation = QuaternionToEuler(linkPose.Orientation);
                return (localCom, orientation);
            }

            private void AddGeometry(XElement link, string rigidBodyName, Geometry geometry, Pose linkPose, RigidBody rigidBody)
            {
                var geometryName = $"{rigidBodyName}_geometry";
                _geometriesAndNames.Add((geometry, geometryName));

                var collision = new XElement("collision",
                    new XElement("geometry", geometry switch
                    {
                        GeometryBox box => new XElement("box", new XAttribute("size", $"{box.AABB.Size.X} {box.AABB.Size.Y} {box.AABB.Size.Z}")),
                        GeometrySphere sphere => new XElement("sphere", new XAttribute("radius", $"{sphere.Radius}")),
                        _ => throw new InvalidOperationException("Unsupported geometry type.")
                    }),
                    new XElement("origin", new XAttribute("xyz", "0 0 0"), new XAttribute("rpy", "0 0 0")));
                link.Add(collision);
            }

            private void AddJoint(List<XElement> elements, Joint joint, JointHinge hingeJoint, string rigidBodyName, Pose linkPose, RigidBody rigidBody)
            {
                var jointName = $"{rigidBodyName}_joint";
                _jointsAndNames.Add((hingeJoint, jointName));

                var jointElement = new XElement("joint",
                    new XAttribute("name", jointName),
                    new XAttribute("type", "revolute"),
                    new XElement("parent", new XAttribute("link", rigidBodyName)),
                    new XElement("child", new XAttribute("link", $"{rigidBodyName}_child")),
                    new XElement("origin", new XAttribute("xyz", "0 0 0"), new XAttribute("rpy", "0 0 0")),
                    new XElement("axis", new XAttribute("xyz", "0 1 0")),
                    new XElement("limit",
                        new XAttribute("lower", $"{-hingeJoint.Range}"),
                        new XAttribute("upper", $"{hingeJoint.Range}"),
                        new XAttribute("effort", $"{hingeJoint.Effort}"),
                        new XAttribute("velocity", $"{hingeJoint.Velocity}")));
                elements.Add(jointElement);
            }

            private static Vector3 QuaternionToEuler(Quaternion quaternion)
            {
                // Converts a quaternion to Euler angles
                var rotationMatrix = Matrix4x4.CreateFromQuaternion(quaternion);
                var euler = new Vector3
                {
                    X = MathF.Atan2(rotationMatrix.M32, rotationMatrix.M33),
                    Y = MathF.Asin(-rotationMatrix.M31),
                    Z = MathF.Atan2(rotationMatrix.M21, rotationMatrix.M11)
                };
                return euler;
            }
        }
    }
}
