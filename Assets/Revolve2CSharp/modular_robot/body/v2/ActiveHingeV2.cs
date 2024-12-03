using System;
using System.Collections.Generic;
using System.Numerics;

namespace Revolve2.Robot
{
    /// <summary>
    /// Represents an Active Hinge V2 module for a modular robot.
    /// This is a rotary joint.
    /// </summary>
    public class ActiveHingeV2 : ActiveHinge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveHingeV2"/> class.
        /// </summary>
        /// <param name="rotation">The module's rotation.</param>
        public ActiveHingeV2(float rotation)
            : base(
                rotation: rotation,
                range: 1.047197551f, // 60 degrees in radians
                effort: 0.948013269f,
                velocity: 6.338968228f,
                frameBoundingBox: new Vector3(0.018f, 0.052f, 0.0165891f),
                frameOffset: 0.04495f,
                servo1BoundingBox: new Vector3(0.05125f, 0.0512f, 0.02f),
                servo2BoundingBox: new Vector3(0.002f, 0.052f, 0.052f),
                frameMass: 0.01632f,
                servo1Mass: 0.058f,
                servo2Mass: 0.025f,
                servoOffset: 0.0239f,
                jointOffset: 0.0119f,
                staticFriction: 1.0f,
                dynamicFriction: 1.0f,
                armature: 0.002f,
                pidGainP: 5.0f,
                pidGainD: 0.05f,
                childOffset: 0.05125f / 2 + 0.002f + 0.01f,
                sensors: new List<Sensor> { new ActiveHingeSensor() }
            )
        {
        }
    }
}
