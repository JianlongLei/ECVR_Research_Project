using System;
using Revolve2.Simulation;

namespace Revolve2.Sensors
{
    /// <summary>
    /// Represents an inertial measurement unit (IMU) sensor.
    /// Reports specific force (related to acceleration), angular rate (related to angular velocity), and orientation.
    /// </summary>
    public class IMUSensor : Sensor
    {
        /// <summary>
        /// Gets or sets the pose of the IMU sensor relative to its parent rigid body.
        /// </summary>
        public Pose Pose { get; set; }

        /// <summary>
        /// Gets the type of the sensor for serialization or identification purposes.
        /// </summary>
        public string Type { get; } = "imu";

        /// <summary>
        /// Initializes a new instance of the <see cref="IMUSensor"/> class.
        /// </summary>
        /// <param name="pose">The pose of the sensor.</param>
        public IMUSensor(Pose pose)
        {
            Pose = pose ?? throw new ArgumentNullException(nameof(pose));
        }
    }
}
