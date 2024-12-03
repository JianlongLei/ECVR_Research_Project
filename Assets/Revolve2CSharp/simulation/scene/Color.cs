namespace Revolve2.Utilities
{
    /// <summary>
    /// Represents a color in RGBA format.
    /// </summary>
    public class Color
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }
        public int Alpha { get; }

        public Color(int red, int green, int blue, int alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        /// <summary>
        /// Converts to a normalized RGBA list (values between 0 and 1).
        /// </summary>
        public float[] ToNormalizedRgbaList()
        {
            return new float[] { Red / 255f, Green / 255f, Blue / 255f, Alpha / 255f };
        }

        /// <summary>
        /// Converts to a normalized RGB list (values between 0 and 1).
        /// </summary>
        public float[] ToNormalizedRgbList()
        {
            return new float[] { Red / 255f, Green / 255f, Blue / 255f };
        }
    }
}
