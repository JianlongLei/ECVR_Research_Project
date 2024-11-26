using System.Numerics;

namespace ModularRobot.Simulation
{
    /// <summary>
    /// Represents an axis-aligned bounding box (AABB).
    /// </summary>
    public class AABB
    {
        /// <summary>
        /// The size of the bounding box (not half the size).
        /// </summary>
        public Vector3 Size { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AABB"/> class.
        /// </summary>
        /// <param name="size">The size of the bounding box.</param>
        public AABB(Vector3 size)
        {
            Size = size;
        }
    }
}
