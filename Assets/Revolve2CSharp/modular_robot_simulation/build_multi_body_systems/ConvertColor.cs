using System;

namespace ModularRobot.Converters
{
    /// <summary>
    /// Converts modular robot colors to simulation-compatible colors.
    /// </summary>
    public static class ConvertColor
    {
        public static Color Convert(Color color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));
            return color;
        }
    }
}
