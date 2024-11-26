using System;
using ModularRobot.Simulation;

namespace ModularRobot.Joints
{
    /// <summary>
    /// Base class for all joints.
    /// </summary>
    public abstract class Joint
    {
        public Guid Uuid { get; } = Guid.NewGuid();

        public Pose Pose { get; set; }

        public RigidBody RigidBody1 { get; set; }

        public RigidBody RigidBody2 { get; set; }

        protected Joint(Pose pose, RigidBody rigidBody1, RigidBody rigidBody2)
        {
            Pose = pose;
            RigidBody1 = rigidBody1;
            RigidBody2 = rigidBody2;
        }
    }
}
