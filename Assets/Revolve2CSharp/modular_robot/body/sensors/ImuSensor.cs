using System.Numerics;

namespace ModularRobot
{
    public class IMUSensor : Sensor
    {
        /// <summary>
        /// Represents an Inertial Measurement Unit (IMU).
        /// Reports specific force (closely related to acceleration), angular rate (closely related to angular velocity), and orientation.
        /// </summary>
        /// <param name="position">The position of the IMU.</param>
        /// <param name="orientation">The orientation of the IMU. Defaults to identity quaternion.</param>
        public IMUSensor(Vector3 position, Quaternion orientation = default)
            : base(orientation, position)
        {
        }
    }
}
