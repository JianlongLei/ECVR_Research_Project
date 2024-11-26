using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ModularRobot.Geo;

namespace ModularRobot.Simulation
{
    /// <summary>
    /// A collection of geometries and physics parameters for a rigid body.
    /// </summary>
    public class RigidBody
    {
        private readonly Guid _uuid;
        public Pose InitialPose { get; }
        public float StaticFriction { get; }
        public float DynamicFriction { get; }
        public List<Geometry> Geometries { get; }
        public AttachedSensors Sensors { get; }

        public Guid UUID => _uuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="RigidBody"/> class.
        /// </summary>
        /// <param name="initialPose">The initial pose of the rigid body relative to its parent system.</param>
        /// <param name="staticFriction">The static friction coefficient of the rigid body.</param>
        /// <param name="dynamicFriction">The dynamic friction coefficient of the rigid body.</param>
        /// <param name="geometries">A list of geometries describing the shape of the rigid body.</param>
        public RigidBody(Pose initialPose, float staticFriction, float dynamicFriction, List<Geometry> geometries)
        {
            _uuid = Guid.NewGuid();
            InitialPose = initialPose ?? throw new ArgumentNullException(nameof(initialPose));
            StaticFriction = staticFriction;
            DynamicFriction = dynamicFriction;
            Geometries = geometries ?? throw new ArgumentNullException(nameof(geometries));
            Sensors = new AttachedSensors();
        }

        /// <summary>
        /// Calculates the mass of the rigid body.
        /// </summary>
        public float Mass => Geometries.Sum(geometry => geometry.Mass);

        /// <summary>
        /// Calculates the center of mass in the local reference frame of the rigid body.
        /// </summary>
        public Vector3 CenterOfMass()
        {
            if (Mass == 0)
                return Geometries.Aggregate(Vector3.Zero, (acc, geometry) => acc + geometry.Pose.Position) / Geometries.Count;

            return Geometries.Aggregate(Vector3.Zero, (acc, geometry) =>
                acc + geometry.Mass * geometry.Pose.Position) / Mass;
        }

        /// <summary>
        /// Calculates the inertia tensor in the local reference frame of the rigid body.
        /// </summary>
        public Matrix4x4 InertiaTensor()
        {
            var centerOfMass = CenterOfMass();
            var inertiaTensor = Matrix4x4.Identity;

            foreach (var geometry in Geometries)
            {
                if (geometry.Mass == 0) continue;

                Matrix4x4 localInertia = geometry switch
                {
                    GeometryBox box => CalculateBoxInertia(box),
                    GeometrySphere sphere => CalculateSphereInertia(sphere),
                    _ => throw new InvalidOperationException(
                        $"Unsupported geometry type: {geometry.GetType().Name}")
                };

                var translation = Matrix4x4.Identity;
                translation.M11 += geometry.Mass * (MathF.Pow(geometry.Pose.Position.Y - centerOfMass.Y, 2) +
                                                    MathF.Pow(geometry.Pose.Position.Z - centerOfMass.Z, 2));
                translation.M22 += geometry.Mass * (MathF.Pow(geometry.Pose.Position.X - centerOfMass.X, 2) +
                                                    MathF.Pow(geometry.Pose.Position.Z - centerOfMass.Z, 2));
                translation.M33 += geometry.Mass * (MathF.Pow(geometry.Pose.Position.X - centerOfMass.X, 2) +
                                                    MathF.Pow(geometry.Pose.Position.Y - centerOfMass.Y, 2));

                var orientationMatrix = QuaternionToRotationMatrix(geometry.Pose.Orientation);
                var globalInertia = orientationMatrix * localInertia * Matrix4x4.Transpose(orientationMatrix) + translation;
                inertiaTensor += globalInertia;
            }

            return inertiaTensor;
        }

        private static Matrix4x4 CalculateBoxInertia(GeometryBox geometry)
        {
            var localInertia = Matrix4x4.Identity;
            localInertia.M11 += geometry.Mass * (MathF.Pow(geometry.AABB.Size.Y, 2) + MathF.Pow(geometry.AABB.Size.Z, 2)) / 12.0f;
            localInertia.M22 += geometry.Mass * (MathF.Pow(geometry.AABB.Size.X, 2) + MathF.Pow(geometry.AABB.Size.Z, 2)) / 12.0f;
            localInertia.M33 += geometry.Mass * (MathF.Pow(geometry.AABB.Size.X, 2) + MathF.Pow(geometry.AABB.Size.Y, 2)) / 12.0f;
            return localInertia;
        }

        private static Matrix4x4 CalculateSphereInertia(GeometrySphere geometry)
        {
            var localInertia = Matrix4x4.Identity;
            localInertia.M11 = localInertia.M22 = localInertia.M33 =
                2 * geometry.Mass * MathF.Pow(geometry.Radius, 2) / 5.0f;
            return localInertia;
        }

        private static Matrix4x4 QuaternionToRotationMatrix(Quaternion quaternion)
        {
            float q0 = quaternion.W;
            float q1 = quaternion.X;
            float q2 = quaternion.Y;
            float q3 = quaternion.Z;

            return new Matrix4x4(
                1 - 2 * (q2 * q2 + q3 * q3), 2 * (q1 * q2 - q0 * q3), 2 * (q1 * q3 + q0 * q2), 0,
                2 * (q1 * q2 + q0 * q3), 1 - 2 * (q1 * q1 + q3 * q3), 2 * (q2 * q3 - q0 * q1), 0,
                2 * (q1 * q3 - q0 * q2), 2 * (q2 * q3 + q0 * q1), 1 - 2 * (q1 * q1 + q2 * q2), 0,
                0, 0, 0, 1);
        }
    }

    /// <summary>
    /// Represents sensors attached to a rigid body.
    /// </summary>
    public class AttachedSensors
    {
        public List<IMUSensor> IMUSensors { get; } = new();
        public List<CameraSensor> CameraSensors { get; } = new();

        public void AddSensor(Sensor sensor)
        {
            switch (sensor)
            {
                case IMUSensor imu:
                    IMUSensors.Add(imu);
                    break;
                case CameraSensor camera:
                    CameraSensors.Add(camera);
                    break;
                default:
                    throw new ArgumentException($"Unsupported sensor type: {sensor.GetType().Name}");
            }
        }
    }
}
