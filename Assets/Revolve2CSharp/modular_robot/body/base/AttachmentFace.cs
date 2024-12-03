using System;
using System.Collections.Generic;
using System.Numerics;

namespace Revolve2.Robot
{
    /// <summary>
    /// Represents a collection of attachment points on a module's face.
    /// This can be thought of as a pseudo-module that usually does not have a body on its own.
    /// </summary>
    public class AttachmentFace : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentFace"/> class.
        /// </summary>
        /// <param name="rotation">The orientation of this face relative to its parent.</param>
        /// <param name="attachmentPoints">The attachment points available on this face.</param>
        public AttachmentFace(float rotation, Dictionary<int, AttachmentPoint> attachmentPoints)
            : base(
                CreateOrientation(rotation),
                new Color(255, 255, 255, 255),
                attachmentPoints,
                new List<Sensor>()
            )
        {
        }

        private static Quaternion CreateOrientation(float rotation)
        {
            return Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rotation);
        }
    }
}
