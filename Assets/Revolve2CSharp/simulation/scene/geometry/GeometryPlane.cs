using ModularRobot.Vector;
using ModularRobot.Simulation;

namespace ModularRobot.Geo
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
    }
}
