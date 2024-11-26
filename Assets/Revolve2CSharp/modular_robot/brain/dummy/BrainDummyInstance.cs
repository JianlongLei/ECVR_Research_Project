namespace ModularRobot
{
    /// <summary>
    /// A brain instance that does nothing.
    /// </summary>
    public class BrainDummyInstance : BrainInstance
    {
        /// <summary>
        /// Performs no control action.
        /// </summary>
        /// <param name="dt">Elapsed seconds since the last call to this function.</param>
        /// <param name="sensorState">Interface for reading the current sensor state.</param>
        /// <param name="controlInterface">Interface for controlling the robot.</param>
        public override void Control(float dt, ModularRobotSensorState sensorState, IModularRobotControlInterface controlInterface)
        {
            // This brain does nothing for control.
        }
    }
}
