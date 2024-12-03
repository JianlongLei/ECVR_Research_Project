using System;
using Revolve2.Simulation;

namespace Revolve2.Geo
{
    /// <summary>
    /// Represents a box-shaped geometry.
    /// </summary>
    public class GeometryBox : Geometry
    {
        public AABB AABB { get; set; }

        public GeometryBox(Pose pose, float mass, Texture texture, AABB aabb)
            : base(pose, mass, texture)
        {
            AABB = aabb ?? throw new ArgumentNullException(nameof(aabb));
        }
    }
}
