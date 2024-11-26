using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ModularRobot.Geo;
using ModularRobot.Joints;
using ModularRobot.Utilities;

namespace ModularRobot.Simulation
{
    /// <summary>
    /// Represents a graph of interconnected rigid bodies, joints, and objects such as cameras.
    /// </summary>
    public class MultiBodySystem
    {
        private readonly Guid _uuid;
        private readonly List<RigidBody> _rigidBodies;
        private readonly Dictionary<UUIDKey<RigidBody>, int> _rigidBodyToIndex;
        private readonly List<Joint?> _halfAdjacencyMatrix;

        public Pose Pose { get; set; }
        public bool IsStatic { get; set; }

        public Guid UUID => _uuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiBodySystem"/> class.
        /// </summary>
        /// <param name="pose">The pose of the system.</param>
        /// <param name="isStatic">Whether the root rigid body is static.</param>
        public MultiBodySystem(Pose pose, bool isStatic)
        {
            _uuid = Guid.NewGuid();
            Pose = pose ?? throw new ArgumentNullException(nameof(pose));
            IsStatic = isStatic;
            _rigidBodies = new List<RigidBody>();
            _rigidBodyToIndex = new Dictionary<UUIDKey<RigidBody>, int>();
            _halfAdjacencyMatrix = new List<Joint?>();
        }

        private int CalculateHalfMatrixIndex(int index1, int index2)
        {
            if (index1 == index2)
                throw new InvalidOperationException("Cannot calculate matrix index for the same rigid body.");

            var smallest = Math.Min(index1, index2);
            var largest = Math.Max(index1, index2);
            var baseIndex = ((largest - 1) * largest / 2) + (largest - 1);

            return baseIndex - smallest;
        }

        public void AddRigidBody(RigidBody rigidBody)
        {
            var key = new UUIDKey<RigidBody>(rigidBody);

            if (_rigidBodyToIndex.ContainsKey(key))
                throw new InvalidOperationException("Rigid body already part of this multi-body system.");

            // Extend adjacency matrix
            _halfAdjacencyMatrix.AddRange(new Joint?[_rigidBodies.Count]);

            // Add rigid body
            _rigidBodyToIndex[key] = _rigidBodies.Count;
            _rigidBodies.Add(rigidBody);
        }

        public void AddJoint(Joint joint)
        {
            if (!_rigidBodyToIndex.TryGetValue(new UUIDKey<RigidBody>(joint.RigidBody1), out var index1))
                throw new InvalidOperationException("First rigid body is not part of this multi-body system.");
            if (!_rigidBodyToIndex.TryGetValue(new UUIDKey<RigidBody>(joint.RigidBody2), out var index2))
                throw new InvalidOperationException("Second rigid body is not part of this multi-body system.");
            if (index1 == index2)
                throw new InvalidOperationException("Cannot create a joint between the same rigid body.");

            // Get the index in the adjacency matrix
            var matrixIndex = CalculateHalfMatrixIndex(index1, index2);
            if (_halfAdjacencyMatrix[matrixIndex] != null)
                throw new InvalidOperationException("A joint already exists between these two rigid bodies.");

            // Assign the joint in the adjacency matrix
            _halfAdjacencyMatrix[matrixIndex] = joint;
        }

        public bool HasRoot()
        {
            return _rigidBodies.Count > 0;
        }

        public RigidBody Root
        {
            get
            {
                if (_rigidBodies.Count == 0)
                    throw new InvalidOperationException("Root has not been added yet.");
                return _rigidBodies[0];
            }
        }

        public List<Joint> GetJointsForRigidBody(RigidBody rigidBody)
        {
            if (!_rigidBodyToIndex.TryGetValue(new UUIDKey<RigidBody>(rigidBody), out var index))
                throw new InvalidOperationException("Rigid body is not part of this multi-body system.");

            var joints = new List<Joint>();
            for (int i = 0; i < _rigidBodies.Count; i++)
            {
                if (i == index) continue;

                var matrixIndex = CalculateHalfMatrixIndex(index, i);
                if (_halfAdjacencyMatrix[matrixIndex] != null)
                {
                    joints.Add(_halfAdjacencyMatrix[matrixIndex]!);
                }
            }

            return joints;
        }

        public (Vector3 Position, AABB AABB) CalculateAABB()
        {
            var points = new List<Vector3>();

            foreach (var rigidBody in _rigidBodies)
            {
                foreach (var geometry in rigidBody.Geometries)
                {
                    if (geometry is not GeometryBox box)
                        throw new InvalidOperationException("AABB calculation currently only supports GeometryBox.");

                    foreach (var sign in new[] { 0.5f, -0.5f })
                    {
                        var transformedPoint = rigidBody.InitialPose.Position
                            + Vector3.Transform(
                                box.Pose.Position + Vector3.Transform(box.AABB.Size * sign, box.Pose.Orientation),
                                rigidBody.InitialPose.Orientation);
                        points.Add(transformedPoint);
                    }
                }
            }

            var min = new Vector3(points.Min(p => p.X), points.Min(p => p.Y), points.Min(p => p.Z));
            var max = new Vector3(points.Max(p => p.X), points.Max(p => p.Y), points.Max(p => p.Z));
            var center = (max + min) / 2;
            var size = max - min;

            return (center, new AABB(size));
        }
    }
}
