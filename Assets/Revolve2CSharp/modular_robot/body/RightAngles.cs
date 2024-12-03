using System;

namespace Revolve2.Robot
{
    /// <summary>
    /// Standard angles at which some modular robot modules can be attached.
    /// </summary>
    public static class RightAngles
    {
        /// <summary>
        /// 0 degrees (0 radians).
        /// </summary>
        public const float Deg0 = 0;

        /// <summary>
        /// 90 degrees (π/2 radians).
        /// </summary>
        public const float Deg90 = MathF.PI / 2.0f;

        /// <summary>
        /// 180 degrees (π radians).
        /// </summary>
        public const float Deg180 = MathF.PI;

        /// <summary>
        /// 270 degrees (3π/2 radians).
        /// </summary>
        public const float Deg270 = MathF.PI * 1.5f;

        /// <summary>
        /// 0 radians.
        /// </summary>
        public const float Rad0 = 0;

        /// <summary>
        /// π/2 radians.
        /// </summary>
        public const float RadHalfPi = MathF.PI / 2.0f;

        /// <summary>
        /// π radians.
        /// </summary>
        public const float RadPi = MathF.PI;

        /// <summary>
        /// 3π/2 radians.
        /// </summary>
        public const float RadOneAndAHalfPi = MathF.PI * 1.5f;
    }
}
