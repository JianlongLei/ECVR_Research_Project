using System;
using System.Collections.Generic;
using System.Numerics;

namespace ModularRobot
{
    /// <summary>
    /// The core module of a modular robot.
    /// </summary>
    public class Core : Module
    {
        public const int FRONT = 0;
        public const int RIGHT = 1;
        public const int BACK = 2;
        public const int LEFT = 3;

        private Vector3 _boundingBox;
        private float _mass;

        /// <summary>
        /// Gets the mass of the Core (in kg).
        /// </summary>
        public float Mass => _mass;

        /// <summary>
        /// Gets the bounding box of the Core.
        /// Sizes are total length, not half-length from origin.
        /// </summary>
        public Vector3 BoundingBox => _boundingBox;

        /// <summary>
        /// Gets or sets the front module of the Core.
        /// </summary>
        public Module? Front
        {
            get => Children.ContainsKey(FRONT) ? Children[FRONT] : null;
            set => SetChild(value, FRONT);
        }

        /// <summary>
        /// Gets or sets the right module of the Core.
        /// </summary>
        public Module? Right
        {
            get => Children.ContainsKey(RIGHT) ? Children[RIGHT] : null;
            set => SetChild(value, RIGHT);
        }

        /// <summary>
        /// Gets or sets the back module of the Core.
        /// </summary>
        public Module? Back
        {
            get => Children.ContainsKey(BACK) ? Children[BACK] : null;
            set => SetChild(value, BACK);
        }

        /// <summary>
        /// Gets or sets the left module of the Core.
        /// </summary>
        public Module? Left
        {
            get => Children.ContainsKey(LEFT) ? Children[LEFT] : null;
            set => SetChild(value, LEFT);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Core"/> class.
        /// </summary>
        /// <param name="rotation">The Core's rotation.</param>
        /// <param name="mass">The Core's mass (in kg).</param>
        /// <param name="boundingBox">The bounding box of the Core (in x, y, z dimensions).</param>
        /// <param name="childOffset">The offset for the children attachment points.</param>
        /// <param name="sensors">The sensors associated with the Core.</param>
        public Core(
            float rotation,
            float mass,
            Vector3 boundingBox,
            float childOffset,
            List<Sensor> sensors
        ) : base(CreateOrientation(rotation), new Color(255, 50, 50, 255), CreateAttachmentPoints(childOffset), sensors)
        {
            _mass = mass;
            _boundingBox = boundingBox;
        }

        private static Quaternion CreateOrientation(float rotation)
        {
            return Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rotation);
        }

        private static Dictionary<int, AttachmentPoint> CreateAttachmentPoints(float childOffset)
        {
            return new Dictionary<int, AttachmentPoint>
            {
                { FRONT, new AttachmentPoint(Quaternion.Identity, new Vector3(childOffset, 0.0f, 0.0f)) },
                { BACK, new AttachmentPoint(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathF.PI), new Vector3(childOffset, 0.0f, 0.0f)) },
                { LEFT, new AttachmentPoint(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathF.PI / 2.0f), new Vector3(childOffset, 0.0f, 0.0f)) },
                { RIGHT, new AttachmentPoint(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathF.PI * 1.5f), new Vector3(childOffset, 0.0f, 0.0f)) }
            };
        }
    }
}
