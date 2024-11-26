using System;
using System.Numerics;
using ModularRobot.Simulation;

namespace ModularRobot.Geo
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
    }
}
