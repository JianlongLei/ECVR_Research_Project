using System;

namespace ModularRobot
{
    /// <summary>
    /// Standard angles at which some modular robot modules can be attached.
    /// </summary>
    public enum RightAngles
    {
        /// <summary>
        /// 0 degrees (0 radians).
        /// </summary>
        Deg0 = 0,

        /// <summary>
        /// 90 degrees (π/2 radians).
        /// </summary>
        Deg90 = (int)(MathF.PI / 2.0f),

        /// <summary>
        /// 180 degrees (π radians).
        /// </summary>
        Deg180 = (int)MathF.PI,

        /// <summary>
        /// 270 degrees (3π/2 radians).
        /// </summary>
        Deg270 = (int)(MathF.PI * 1.5f),

        /// <summary>
        /// 0 radians.
        /// </summary>
        Rad0 = 0,

        /// <summary>
        /// π/2 radians.
        /// </summary>
        RadHalfPi = (int)(MathF.PI / 2.0f),

        /// <summary>
        /// π radians.
        /// </summary>
        RadPi = (int)MathF.PI,

        /// <summary>
        /// 3π/2 radians.
        /// </summary>
        RadOneAndAHalfPi = (int)(MathF.PI * 1.5f)
    }
}
