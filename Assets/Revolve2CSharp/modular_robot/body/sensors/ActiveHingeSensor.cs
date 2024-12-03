using System.Numerics;

namespace Revolve2.Robot
{
    /// <summary>
    /// A sensor for an active hinge that measures its angle.
    /// </summary>
    public class ActiveHingeSensor : Sensor
    {
        /// <summary>
        /// Initializes the ActiveHinge sensor with a default orientation and position.
        /// </summary>
        public ActiveHingeSensor()
            : base(Quaternion.Identity, Vector3.Zero)
        {
        }
    }
}
