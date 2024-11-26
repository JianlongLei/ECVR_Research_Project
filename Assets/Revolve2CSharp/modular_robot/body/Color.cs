namespace ModularRobot
{
    /// <summary>
    /// Represents a color in RGBA format.
    /// All values should be between 0 and 255.
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Red component of the color.
        /// </summary>
        public int Red { get; set; }

        /// <summary>
        /// Green component of the color.
        /// </summary>
        public int Green { get; set; }

        /// <summary>
        /// Blue component of the color.
        /// </summary>
        public int Blue { get; set; }

        /// <summary>
        /// Alpha (transparency) component of the color.
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="red">Red component (0-255).</param>
        /// <param name="green">Green component (0-255).</param>
        /// <param name="blue">Blue component (0-255).</param>
        /// <param name="alpha">Alpha component (0-255).</param>
        public Color(int red, int green, int blue, int alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        /// <summary>
        /// Converts the color to a normalized RGBA list where each value is between 0 and 1.
        /// </summary>
        /// <returns>A list of normalized RGBA values.</returns>
        public float[] ToNormalizedRgbaList()
        {
            return new[]
            {
                Red / 255f,
                Green / 255f,
                Blue / 255f,
                Alpha / 255f
            };
        }
    }
}
