using System;
using ModularRobot.Simulation;

namespace ModularRobot.Sensors
{
    /// <summary>
    /// Represents a camera sensor.
    /// </summary>
    public class CameraSensor : Sensor
    {
        /// <summary>
        /// Gets or sets the pose of the camera sensor relative to its parent rigid body.
        /// </summary>
        public Pose Pose { get; set; }

        /// <summary>
        /// Gets or sets the camera resolution (width and height).
        /// </summary>
        public (int Width, int Height) CameraSize { get; set; }

        /// <summary>
        /// Gets the type of the sensor for serialization or identification purposes.
        /// </summary>
        public string Type { get; } = "camera";

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraSensor"/> class.
        /// </summary>
        /// <param name="pose">The pose of the sensor.</param>
        /// <param name="cameraSize">The camera resolution.</param>
        public CameraSensor(Pose pose, (int Width, int Height) cameraSize)
        {
            Pose = pose ?? throw new ArgumentNullException(nameof(pose));
            CameraSize = cameraSize;
        }
    }
}
