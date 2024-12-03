using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Revolve2.Simulation;
using Revolve2.Utilities;
using Revolve2.Mapping;
using Revolve2.Geo;
using System.Numerics;
using Revolve2.Factories;
using Revolve2.Robot;

namespace Revolve2.Converters
{
    /// <summary>
    /// Converts modular robot bodies to multi-body systems for simulation.
    /// </summary>
    public class BodyToMultiBodySystemConverter
    {
        private const float StaticFriction = 1.0f;
        private const float DynamicFriction = 1.0f;

        /// <summary>
        /// Converts a modular robot body to a multi-body system.
        /// </summary>
        /// <param name="body">The body to convert.</param>
        /// <param name="pose">The pose of the multi-body system.</param>
        /// <param name="translateZAABB">If true, aligns the robot's bounding box with the ground.</param>
        /// <returns>A tuple containing the multi-body system and its mapping.</returns>
        public (MultiBodySystem MultiBodySystem, BodyToMultiBodySystemMapping Mapping)
            ConvertRobotBody(Body body, Pose pose, bool translateZAABB)
        {
            if (body == null) throw new ArgumentNullException(nameof(body));
            if (pose == null) throw new ArgumentNullException(nameof(pose));

            var multiBodySystem = new MultiBodySystem(pose, isStatic: false);

            var rigidBody = new RigidBody(
                new Pose(),
                StaticFriction,
                DynamicFriction,
                new List<Geometry>()
            );

            var mapping = new BodyToMultiBodySystemMapping(multiBodySystem);
            multiBodySystem.AddRigidBody(rigidBody);

            var queue = new Queue<UnbuiltChild>();
            queue.Enqueue(new UnbuiltChild(body.Core, rigidBody));

            while (queue.Count > 0)
            {
                var unbuilt = queue.Dequeue();
                var builder = BuilderFactory.GetBuilder(unbuilt);
                var newTasks = builder.Build(multiBodySystem, mapping);
                foreach (var task in newTasks)
                {
                    queue.Enqueue(task);
                }
            }

            if (translateZAABB)
            {
                var (aabbPosition, aabb) = multiBodySystem.CalculateAABB();
                pose.Position += new Vector3(0.0f, 0.0f, -aabbPosition.Z + aabb.Size.Z / 2.0f);
            }

            return (multiBodySystem, mapping);
        }
    }
}
