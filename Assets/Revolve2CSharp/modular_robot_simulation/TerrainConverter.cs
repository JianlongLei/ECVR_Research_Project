using System.Collections.Generic;
using Revolve2.Geo;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Utility for converting terrain into a multi-body system.
    /// </summary>
    public static class TerrainConverter
    {
        /// <summary>
        /// Converts a terrain into a multi-body system.
        /// </summary>
        /// <param name="terrain">The terrain to convert.</param>
        /// <returns>The resulting multi-body system.</returns>
        public static MultiBodySystem ConvertTerrain(Terrain terrain)
        {
            var multiBodySystem = new MultiBodySystem(new Pose(), true);
            var rigidBody = new RigidBody(
                new Pose(),
                staticFriction: 1.0f,
                dynamicFriction: 1.0f,
                geometries: new List<Geometry>()
            );

            foreach (var geometry in terrain.StaticGeometry)
            {
                rigidBody.Geometries.Add(geometry);
            }

            multiBodySystem.AddRigidBody(rigidBody);
            return multiBodySystem;
        }
    }
}
