using System.Numerics;

namespace ModularRobot.Sensors
{
    /// <summary>
    /// Represents the state of an IMU (Inertial Measurement Unit) sensor.
    /// </summary>
    public abstract class IMUSensorState
    {
        /// <summary>
        /// Gets the measured specific force.
        /// </summary>
        public abstract Vector3 SpecificForce { get; }

        /// <summary>
        /// Gets the measured angular rate.
        /// </summary>
        public abstract Vector3 AngularRate { get; }

        /// <summary>
        /// Gets the measured orientation.
        /// </summary>
        public abstract Vector3 Orientation { get; }
    }
}
