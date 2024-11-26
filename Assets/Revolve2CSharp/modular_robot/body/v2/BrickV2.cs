using System.Collections.Generic;
using System.Numerics;

namespace ModularRobot
{
    /// <summary>
    /// Represents a BrickV2 module for a modular robot.
    /// </summary>
    public class BrickV2 : Brick
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrickV2"/> class.
        /// </summary>
        /// <param name="rotation">The module's rotation.</param>
        public BrickV2(float rotation)
            : base(
                rotation: rotation,
                boundingBox: new Vector3(0.075f, 0.075f, 0.075f),
                mass: 0.06043f,
                childOffset: 0.075f / 2.0f,
                sensors: new List<Sensor>()
            )
        {
        }
    }
}
