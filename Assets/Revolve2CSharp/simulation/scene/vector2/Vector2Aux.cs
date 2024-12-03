using System;
using System.Numerics;

namespace Revolve2.Vector
{
    /// <summary>
    /// Provides utility methods for creating and manipulating 2D vectors.
    /// </summary>
    public static class Vector2Aux
    {
        /// <summary>
        /// Creates a 2D vector.
        /// </summary>
        public static Vector2 Create(float x = 0.0f, float y = 0.0f)
        {
            return new Vector2(x, y);
        }

        /// <summary>
        /// Creates a unit vector along the X-axis.
        /// </summary>
        public static Vector2 CreateUnitX()
        {
            return new Vector2(1.0f, 0.0f);
        }

        /// <summary>
        /// Creates a unit vector along the Y-axis.
        /// </summary>
        public static Vector2 CreateUnitY()
        {
            return new Vector2(0.0f, 1.0f);
        }

        /// <summary>
        /// Creates a 2D vector from a 3x3 matrix's translation component.
        /// </summary>
        public static Vector2 CreateFromMatrix33Translation(Matrix3x2 matrix)
        {
            return new Vector2(matrix.M31, matrix.M32);
        }

        /// <summary>
        /// Provides unit vectors for common directions.
        /// </summary>
        public static class Unit
        {
            public static readonly Vector2 X = CreateUnitX();
            public static readonly Vector2 Y = CreateUnitY();
        }

        /// <summary>
        /// Provides index positions for accessing vector components.
        /// </summary>
        public static class Index
        {
            public const int X = 0;
            public const int Y = 1;
        }
    }
}
