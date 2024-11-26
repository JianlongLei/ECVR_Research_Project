using System;
using System.Collections.Generic;
using ModularRobot.Geo;
using ModularRobot.Simulation;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Utility to convert terrain into a multi-body system for simulation.
    /// </summary>
    public static class ConvertTerrain
    {
        /// <summary>
        /// Converts a terrain into a multi-body system.
        /// </summary>
        /// <param name="terrain">The terrain to convert.</param>
        /// <returns>The created multi-body system.</returns>
        public static MultiBodySystem Convert(Terrain terrain)
        {
            if (terrain == null) throw new ArgumentNullException(nameof(terrain));

            // Create a multi-body system with a default static pose
            var multiBodySystem = new MultiBodySystem(new Pose(), isStatic: true);

            // Create a rigid body for the terrain
            var rigidBody = new RigidBody(
                new Pose(),
                staticFriction: 1.0f,
                dynamicFriction: 1.0f,
                new List<Geometry>()
            );

            // Add the rigid body to the multi-body system
            multiBodySystem.AddRigidBody(rigidBody);

            // Attach all static geometries from the terrain to the rigid body
            foreach (var geometry in terrain.StaticGeometry)
            {
                rigidBody.Geometries.Add(geometry);
            }

            return multiBodySystem;
        }
    }
}
