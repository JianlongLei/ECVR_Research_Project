using System;
using System.Numerics;

namespace ModularRobot.Simulation
{
    /// <summary>
    /// Represents the position and orientation of an object.
    /// </summary>
    public class Pose
    {
        public Vector3 Position { get; set; }
        public Quaternion Orientation { get; set; }

        public Pose(Vector3 position, Quaternion orientation)
        {
            Position = position;
            Orientation = orientation;
        }

        public Pose() : this(Vector3.Zero, Quaternion.Identity) { }

    }
}
