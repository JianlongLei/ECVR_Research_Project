namespace Revolve2.Sensors
{
    /// <summary>
    /// Represents the state of an active hinge sensor.
    /// </summary>
    public abstract class ActiveHingeSensorState
    {
        /// <summary>
        /// Gets the measured position of the active hinge.
        /// </summary>
        public abstract float Position { get; }
    }
}
