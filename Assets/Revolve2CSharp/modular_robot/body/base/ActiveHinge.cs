using System;
using System.Collections.Generic;
using System.Numerics;

namespace ModularRobot
{
    /// <summary>
    /// Represents an Active Hinge Module.
    /// </summary>
    public class ActiveHinge : Module
    {
        public const int ATTACHMENT = 0;

        private float _range;
        private float _effort;
        private float _velocity;

        private Vector3 _servo1BoundingBox;
        private Vector3 _servo2BoundingBox;
        private Vector3 _frameBoundingBox;
        private float _frameOffset;
        private float _servoOffset;
        private float _frameMass;
        private float _servo1Mass;
        private float _servo2Mass;
        private float _jointOffset;
        private float _staticFriction;
        private float _dynamicFriction;
        private float _armature;
        private float _pidGainP;
        private float _pidGainD;

        /// <summary>
        /// Gets or sets the module attached to this hinge.
        /// </summary>
        public Module? Attachment
        {
            get => Children.ContainsKey(ATTACHMENT) ? Children[ATTACHMENT] : null;
            set => SetChild(value, ATTACHMENT);
        }

        public float StaticFriction => _staticFriction;
        public float DynamicFriction => _dynamicFriction;
        public float Range => _range;
        public float Effort => _effort;
        public float Velocity => _velocity;
        public Vector3 Servo1BoundingBox => _servo1BoundingBox;
        public Vector3 Servo2BoundingBox => _servo2BoundingBox;
        public Vector3 FrameBoundingBox => _frameBoundingBox;
        public float FrameOffset => _frameOffset;
        public float ServoOffset => _servoOffset;
        public float FrameMass => _frameMass;
        public float Servo1Mass => _servo1Mass;
        public float Servo2Mass => _servo2Mass;
        public float JointOffset => _jointOffset;
        public float Armature => _armature;
        public float PidGainP => _pidGainP;
        public float PidGainD => _pidGainD;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveHinge"/> class.
        /// </summary>
        public ActiveHinge(
            float rotation,
            Vector3 servo1BoundingBox,
            Vector3 servo2BoundingBox,
            Vector3 frameBoundingBox,
            float frameOffset,
            float servoOffset,
            float frameMass,
            float servo1Mass,
            float servo2Mass,
            float jointOffset,
            float staticFriction,
            float dynamicFriction,
            float range,
            float effort,
            float velocity,
            float armature,
            float pidGainP,
            float pidGainD,
            float childOffset,
            List<Sensor> sensors
        ) : base(
            CreateOrientation(rotation),
            new Color(255, 255, 255, 255),
            CreateAttachmentPoints(childOffset),
            sensors
        )
        {
            _staticFriction = staticFriction;
            _dynamicFriction = dynamicFriction;
            _jointOffset = jointOffset;
            _frameOffset = frameOffset;
            _servoOffset = servoOffset;
            _effort = effort;
            _velocity = velocity;
            _range = range;
            _frameMass = frameMass;
            _servo1Mass = servo1Mass;
            _servo2Mass = servo2Mass;
            _frameBoundingBox = frameBoundingBox;
            _servo1BoundingBox = servo1BoundingBox;
            _servo2BoundingBox = servo2BoundingBox;
            _armature = armature;
            _pidGainP = pidGainP;
            _pidGainD = pidGainD;
        }

        private static Quaternion CreateOrientation(float rotation)
        {
            return Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rotation);
        }

        private static Dictionary<int, AttachmentPoint> CreateAttachmentPoints(float childOffset)
        {
            return new Dictionary<int, AttachmentPoint>
            {
                { ATTACHMENT, new AttachmentPoint(Quaternion.Identity, new Vector3(childOffset, 0.0f, 0.0f)) }
            };
        }
    }
}
