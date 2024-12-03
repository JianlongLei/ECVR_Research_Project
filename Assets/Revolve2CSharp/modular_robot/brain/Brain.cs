namespace Revolve2.Robot
{
    /// <summary>
    /// Represents the brain of a modular robot.
    /// This class is an abstract base class. Inherit from this to implement a custom brain.
    /// </summary>
    public abstract class Brain
    {
        /// <summary>
        /// Creates an instance of the brain.
        /// The instance contains all the state associated with the control strategy; this class itself must remain stateless.
        /// </summary>
        /// <returns>A new instance of the brain.</returns>
        public abstract BrainInstance MakeInstance();
    }
}
