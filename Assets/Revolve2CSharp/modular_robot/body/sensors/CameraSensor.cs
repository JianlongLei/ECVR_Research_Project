using System.Numerics;

namespace Revolve2.Robot
{
    public class CameraSensor : Sensor
    {
        private (int Width, int Height) _cameraSize;

        /// <summary>
        /// The size of the camera image. Width and Height tuple.
        /// </summary>
        public (int Width, int Height) CameraSize => _cameraSize;

        /// <summary>
        /// Initializes the Camera Sensor.
        /// Note that the camera size can have a significant impact on performance.
        /// For evolution-related work, stick to 10x10 for fast results.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="orientation">The orientation of the camera. Defaults to identity quaternion.</param>
        /// <param name="cameraSize">The size of the camera image. Defaults to 50x50.</param>
        public CameraSensor(
            Vector3 position,
            Quaternion orientation = default,
            (int Width, int Height)? cameraSize = null
        ) : base(orientation, position)
        {
            _cameraSize = cameraSize ?? (50, 50);
        }
    }
}
