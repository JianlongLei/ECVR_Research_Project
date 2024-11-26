using System;
using ModularRobot.Simulation;
using ModularRobot.Utilities;

namespace ModularRobot.Geo
{
    /// <summary>
    /// Represents a base class for geometries describing parts of a rigid body.
    /// </summary>
    public abstract class Geometry
    {
        public Pose Pose { get; set; }
        public float Mass { get; set; }
        public Texture Texture { get; set; }

        protected Geometry(Pose pose, float mass, Texture texture)
        {
            Pose = pose ?? throw new ArgumentNullException(nameof(pose));
            Mass = mass;
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
        }
    }
}
