using System;
using Revolve2.Utilities;
using RobotColor = Revolve2.Robot.Color;

namespace Revolve2.Converters
{
    /// <summary>
    /// Converts modular robot colors to simulation-compatible colors.
    /// </summary>
    public static class ConvertColor
    {
        public static Color Convert(RobotColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));
            return new Color(color.Red, color.Green, color.Blue, color.Alpha);
        }
    }
}
