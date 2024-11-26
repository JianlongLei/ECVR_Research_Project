using ModularRobot.Joints;

namespace ModularRobot.Simulation
{
    /// <summary>
    /// Interface for controlling a scene during simulation.
    /// </summary>
    public abstract class ControlInterface
    {
        /// <summary>
        /// Sets the position target of a hinge joint.
        /// </summary>
        /// <param name="jointHinge">The hinge joint to control.</param>
        /// <param name="position">The target position.</param>
        public abstract void SetJointHingePositionTarget(JointHinge jointHinge, float position);
    }
}
