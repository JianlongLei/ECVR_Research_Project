using ModularRobot.Sensors;

namespace ModularRobot
{
    /// <summary>
    /// Represents the state of a modular robot's sensors.
    /// </summary>
    public abstract class ModularRobotSensorState
    {
        /// <summary>
        /// Gets the state of the provided active hinge sensor.
        /// </summary>
        /// <param name="sensor">The sensor to get the state for.</param>
        /// <returns>The state of the active hinge sensor.</returns>
        public abstract ActiveHingeSensorState GetActiveHingeSensorState(ActiveHingeSensor sensor);

        /// <summary>
        /// Gets the state of the provided IMU sensor.
        /// </summary>
        /// <param name="sensor">The sensor to get the state for.</param>
        /// <returns>The state of the IMU sensor.</returns>
        public abstract IMUSensorState GetIMUSensorState(IMUSensor sensor);

        /// <summary>
        /// Gets the state of the provided camera sensor.
        /// </summary>
        /// <param name="sensor">The sensor to get the state for.</param>
        /// <returns>The state of the camera sensor.</returns>
        public abstract CameraSensorState GetCameraSensorState(CameraSensor sensor);
    }
}
