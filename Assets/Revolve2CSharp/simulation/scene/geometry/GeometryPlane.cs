using Revolve2.Vector;
using Revolve2.Simulation;
using Revolve2.Utilities;

namespace Revolve2.Geo
{
    /// <summary>
    /// Represents a flat plane geometry.
    /// </summary>
    public class GeometryPlane : Geometry
    {
        public Vector2 Size { get; set; }

        public GeometryPlane(Pose pose, float mass, Texture texture, Vector2 size)
            : base(pose, mass, texture)
        {
            Size = size;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryPlane"/> class.
        /// </summary>
        /// <param name="size">The size of the plane.</param>
        /// <param name="texture">The texture of the plane. Defaults to a gray 2D texture.</param>
        public GeometryPlane(Pose pose, Vector2 size)
            : base(pose, 0.0f, null)
        {
            Size = size;
            Texture = new Texture
            {
                BaseColor = new Color(100, 100, 100, 255),
                MapType = MapType.MAP2D
            };
        }
    }
}
