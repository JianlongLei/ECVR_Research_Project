using System.Numerics;
using Revolve2.Simulation;

namespace Revolve2.Joints
{
    /// <summary>
    /// A hinge joint, also known as a revolute joint, which rotates around a single axis.
    /// </summary>
    public class JointHinge : Joint
    {
        public Vector3 Axis { get; set; }
        public float Range { get; set; }
        public float Effort { get; set; }
        public float Velocity { get; set; }
        public float Armature { get; set; }
        public float PidGainP { get; set; }
        public float PidGainD { get; set; }

        public JointHinge(Pose pose, RigidBody rigidBody1, RigidBody rigidBody2, Vector3 axis, float range,
            float effort, float velocity, float armature, float pidGainP, float pidGainD)
            : base(pose, rigidBody1, rigidBody2)
        {
            Axis = axis;
            Range = range;
            Effort = effort;
            Velocity = velocity;
            Armature = armature;
            PidGainP = pidGainP;
            PidGainD = pidGainD;
        }
    }
}
