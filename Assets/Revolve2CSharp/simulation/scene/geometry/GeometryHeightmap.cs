using System;
using System.Numerics;
using Revolve2.Simulation;
using Revolve2.Utilities;

namespace Revolve2.Geo
{
    /// <summary>
    /// Represents a heightmap geometry.
    /// </summary>
    public class GeometryHeightmap : Geometry
    {
        public Vector3 Size { get; set; }
        public float BaseThickness { get; set; }
        public float[,] Heights { get; set; }

        public GeometryHeightmap(Pose pose, float mass, Texture texture, Vector3 size, float baseThickness, float[,] heights)
            : base(pose, mass, texture)
        {
            Size = size;
            BaseThickness = baseThickness;
            Heights = heights ?? throw new ArgumentNullException(nameof(heights));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryHeightmap"/> class.
        /// </summary>
        /// <param name="size">The size of the heightmap.</param>
        /// <param name="baseThickness">The thickness of the base box.</param>
        /// <param name="heights">A 2D array of height values between 0.0 and 1.0.</param>
        /// <param name="texture">The texture of the heightmap. Defaults to a gray 2D texture.</param>
        public GeometryHeightmap(Pose pose, float mass, Vector3 size, float baseThickness, float[,] heights)
            : base(pose, mass, null)
        {
            Size = size;
            BaseThickness = baseThickness;
            Heights = heights ?? throw new ArgumentNullException(nameof(heights));
            Texture = new Texture
            {
                BaseColor = new Color(100, 100, 100, 255),
                MapType = MapType.MAP2D
            };
        }
    }
}
