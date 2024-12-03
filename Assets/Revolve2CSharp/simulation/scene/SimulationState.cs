using System;
using System.Drawing;
using System.Numerics;
using Revolve2.Joints;
using Revolve2.Sensors;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Interface for the state of a simulation at a certain point.
    /// </summary>
    public abstract class SimulationState
    {
        /// <summary>
        /// Gets the pose of a rigid body, relative to its parent multi-body system's reference frame.
        /// </summary>
        /// <param name="rigidBody">The rigid body to get the pose for.</param>
        /// <returns>The relative pose.</returns>
        public abstract Pose GetRigidBodyRelativePose(RigidBody rigidBody);

        /// <summary>
        /// Gets the pose of a rigid body, relative to the global reference frame.
        /// </summary>
        /// <param name="rigidBody">The rigid body to get the pose for.</param>
        /// <returns>The absolute pose.</returns>
        public abstract Pose GetRigidBodyAbsolutePose(RigidBody rigidBody);

        /// <summary>
        /// Gets the pose of a multi-body system, relative to the global reference frame.
        /// </summary>
        /// <param name="multiBodySystem">The multi-body system to get the pose for.</param>
        /// <returns>The relative pose.</returns>
        public abstract Pose GetMultiBodySystemPose(MultiBodySystem multiBodySystem);

        /// <summary>
        /// Gets the rotational position of a hinge joint.
        /// </summary>
        /// <param name="joint">The joint to get the rotational position for.</param>
        /// <returns>The rotational position.</returns>
        public abstract float GetHingeJointPosition(JointHinge joint);

        /// <summary>
        /// Gets the specific force measured by an IMU.
        /// </summary>
        /// <param name="imuSensor">The IMU sensor.</param>
        /// <returns>The specific force.</returns>
        public abstract Vector3 GetIMUSpecificForce(IMUSensor imuSensor);

        /// <summary>
        /// Gets the angular rate measured by an IMU.
        /// </summary>
        /// <param name="imuSensor">The IMU sensor.</param>
        /// <returns>The angular rate.</returns>
        public abstract Vector3 GetIMUAngularRate(IMUSensor imuSensor);

        /// <summary>
        /// Gets the camera view.
        /// </summary>
        /// <param name="cameraSensor">The camera sensor.</param>
        /// <returns>The view as an image.</returns>
        public abstract byte[,] GetCameraView(CameraSensor cameraSensor);
    }
}
