using Revolve2.Simulation;

namespace Revolve2.Joints
{
    /// <summary>
    /// A joint that fixes two rigid bodies together rigidly.
    /// </summary>
    public class JointFixed : Joint
    {
        public JointFixed(Pose pose, RigidBody rigidBody1, RigidBody rigidBody2)
            : base(pose, rigidBody1, rigidBody2) { }
    }
}
