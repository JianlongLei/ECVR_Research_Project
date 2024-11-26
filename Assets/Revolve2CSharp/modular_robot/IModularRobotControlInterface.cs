namespace ModularRobot
{
    /// <summary>
    /// Interface for controlling modular robots.
    /// </summary>
    public interface IModularRobotControlInterface
    {
        /// <summary>
        /// Sets the position target for an active hinge on the modular robot.
        /// </summary>
        /// <param name="activeHinge">The active hinge object to set the target for.</param>
        /// <param name="target">The target value to set.</param>
        void SetActiveHingeTarget(ActiveHinge activeHinge, float target);
    }
}
