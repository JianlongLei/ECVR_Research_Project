using System;
using System.Collections.Generic;
using System.Numerics;

namespace Revolve2.Robot
{
    /// <summary>
    /// A Brick Module.
    /// </summary>
    public class Brick : Module
    {
        public const int FRONT = 0;
        public const int RIGHT = 1;
        public const int LEFT = 2;

        private float _mass;
        private Vector3 _boundingBox;

        /// <summary>
        /// Gets the mass of the brick (in kg).
        /// </summary>
        public float Mass => _mass;

        /// <summary>
        /// Gets the bounding box size of the brick.
        /// Sizes are total length, not half-length from origin.
        /// </summary>
        public Vector3 BoundingBox => _boundingBox;

        /// <summary>
        /// Gets or sets the front module of the brick.
        /// </summary>
        public Module? Front
        {
            get => Children.ContainsKey(FRONT) ? Children[FRONT] : null;
            set => SetChild(value, FRONT);
        }

        /// <summary>
        /// Gets or sets the right module of the brick.
        /// </summary>
        public Module? Right
        {
            get => Children.ContainsKey(RIGHT) ? Children[RIGHT] : null;
            set => SetChild(value, RIGHT);
        }

        /// <summary>
        /// Gets or sets the left module of the brick.
        /// </summary>
        public Module? Left
        {
            get => Children.ContainsKey(LEFT) ? Children[LEFT] : null;
            set => SetChild(value, LEFT);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
        /// </summary>
        /// <param name="rotation">The Brick's rotation.</param>
        /// <param name="mass">The Brick's mass (in kg).</param>
        /// <param name="boundingBox">The bounding box of the Brick (in x, y, z dimensions).</param>
        /// <param name="childOffset">The offset for the children attachment points.</param>
        /// <param name="sensors">The sensors associated with the Brick.</param>
        public Brick(
            float rotation,
            float mass,
            Vector3 boundingBox,
            float childOffset,
            List<Sensor> sensors
        ) : base(CreateOrientation(rotation), new Color(50, 50, 255, 255), CreateAttachmentPoints(childOffset), sensors)
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
                { LEFT, new AttachmentPoint(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathF.PI / 2.0f), new Vector3(childOffset, 0.0f, 0.0f)) },
                { RIGHT, new AttachmentPoint(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathF.PI * 1.5f), new Vector3(childOffset, 0.0f, 0.0f)) }
            };
        }
    }
}
