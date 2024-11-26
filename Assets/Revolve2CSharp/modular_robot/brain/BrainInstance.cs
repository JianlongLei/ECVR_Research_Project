namespace ModularRobot
{
    /// <summary>
    /// Represents an instance of a brain that performs the control of a modular robot.
    /// Instances of this class can maintain state.
    /// </summary>
    public abstract class BrainInstance
    {
        /// <summary>
        /// Controls the modular robot.
        /// </summary>
        /// <param name="dt">Elapsed time in seconds since the last call to this method.</param>
        /// <param name="sensorState">Interface for reading the current sensor state.</param>
        /// <param name="controlInterface">Interface for controlling the robot.</param>
        public abstract void Control(
            float dt,
            ModularRobotSensorState sensorState,
            IModularRobotControlInterface controlInterface
        );
    }
}
