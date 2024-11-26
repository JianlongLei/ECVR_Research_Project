using System;

namespace ModularRobot.Sensors
{
    /// <summary>
    /// Represents the state of a camera sensor.
    /// </summary>
    public abstract class CameraSensorState
    {
        /// <summary>
        /// Gets the current image captured by the camera.
        /// </summary>
        public abstract byte[,] Image { get; }
    }
}
