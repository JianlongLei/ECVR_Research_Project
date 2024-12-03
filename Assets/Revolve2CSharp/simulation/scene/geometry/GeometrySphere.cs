using Revolve2.Simulation;

namespace Revolve2.Geo
{
    /// <summary>
    /// Represents a sphere-shaped geometry.
    /// </summary>
    public class GeometrySphere : Geometry
    {
        public float Radius { get; set; }

        public GeometrySphere(Pose pose, float mass, Texture texture, float radius)
            : base(pose, mass, texture)
        {
            Radius = radius;
        }
    }
}
