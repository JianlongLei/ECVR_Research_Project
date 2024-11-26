using System;
using System.Numerics;
using ModularRobot.Simulation;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Represents a module or sensor to be built into the multi-body system.
    /// </summary>
    public class UnbuiltChild
    {
        public IOrientation ChildObject { get; }
        public RigidBody RigidBody { get; }
        public Pose Pose { get; private set; }

        public UnbuiltChild(IOrientation childObject, RigidBody rigidBody)
        {
            ChildObject = childObject ?? throw new ArgumentNullException(nameof(childObject));
            RigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
        }

        public void UpdatePose(Vector3 position, Quaternion orientation)
        {

            Pose = new Pose(position, orientation * ChildObject.Orientation);
        }
    }
}
