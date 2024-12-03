using System;
using System.Collections.Generic;
using System.Numerics;

namespace Revolve2.Robot
{
    /// <summary>
    /// Represents the CoreV2 module of a modular robot.
    /// </summary>
    public class CoreV2 : Core
    {
        private const float BatteryMass = 0.39712f; // in kg
        private const float FrameMass = 1.0644f; // in kg
        private const float HorizontalOffset = 0.029f; // in meters
        private const float VerticalOffset = 0.032f; // in meters

        private readonly Dictionary<int, AttachmentFaceCoreV2> _attachmentFaces;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreV2"/> class.
        /// </summary>
        /// <param name="rotation">The module's rotation.</param>
        /// <param name="numBatteries">The number of batteries in the robot.</param>
        public CoreV2(float rotation, int numBatteries = 1)
            : base(
                rotation: rotation,
                mass: numBatteries * BatteryMass + FrameMass,
                boundingBox: new Vector3(0.15f, 0.15f, 0.15f),
                childOffset: 0.0f,
                sensors: new List<Sensor>()
            )
        {
            _attachmentFaces = new Dictionary<int, AttachmentFaceCoreV2>
            {
                { FRONT, new AttachmentFaceCoreV2(0.0f, HorizontalOffset, VerticalOffset) },
                { BACK, new AttachmentFaceCoreV2(MathF.PI, HorizontalOffset, VerticalOffset) },
                { RIGHT, new AttachmentFaceCoreV2(MathF.PI / 2.0f, HorizontalOffset, VerticalOffset) },
                { LEFT, new AttachmentFaceCoreV2(3 * MathF.PI / 2.0f, HorizontalOffset, VerticalOffset) },
            };

            // Set the attachment faces as children of the V2 Core.
            Front = _attachmentFaces[FRONT];
            Back = _attachmentFaces[BACK];
            Right = _attachmentFaces[RIGHT];
            Left = _attachmentFaces[LEFT];
        }

        /// <summary>
        /// Gets the attachment face at the front of the core.
        /// </summary>
        public AttachmentFaceCoreV2 FrontFace => _attachmentFaces[FRONT];

        /// <summary>
        /// Gets the attachment face at the back of the core.
        /// </summary>
        public AttachmentFaceCoreV2 BackFace => _attachmentFaces[BACK];

        /// <summary>
        /// Gets the attachment face at the right of the core.
        /// </summary>
        public AttachmentFaceCoreV2 RightFace => _attachmentFaces[RIGHT];

        /// <summary>
        /// Gets the attachment face at the left of the core.
        /// </summary>
        public AttachmentFaceCoreV2 LeftFace => _attachmentFaces[LEFT];

        /// <summary>
        /// Gets the horizontal offset for attachment positions (in meters).
        /// </summary>
        public float HorizontalOffsetValue => HorizontalOffset;

        /// <summary>
        /// Gets the vertical offset for attachment positions (in meters).
        /// </summary>
        public float VerticalOffsetValue => VerticalOffset;

        /// <summary>
        /// Gets all attachment faces for the core.
        /// </summary>
        public IReadOnlyDictionary<int, AttachmentFaceCoreV2> AttachmentFaces => _attachmentFaces;
    }
}
