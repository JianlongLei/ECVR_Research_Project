using System.Numerics;

namespace ModularRobot
{
    /// <summary>
    /// Represents a theoretical location on the parent module for the child to be attached to.
    /// This is used for potential children to be placed at the correct positions on the current module.
    /// </summary>
    public class AttachmentPoint
    {
        /// <summary>
        /// The orientation of the attachment point on the module.
        /// </summary>
        public Quaternion Orientation { get; set; }

        /// <summary>
        /// The offset for the attachment point with respect to the center of the module.
        /// </summary>
        public Vector3 Offset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentPoint"/> class.
        /// </summary>
        /// <param name="orientation">The orientation of the attachment point.</param>
        /// <param name="offset">The offset of the attachment point.</param>
        public AttachmentPoint(Quaternion orientation, Vector3 offset)
        {
            Orientation = orientation;
            Offset = offset;
        }
    }
}
