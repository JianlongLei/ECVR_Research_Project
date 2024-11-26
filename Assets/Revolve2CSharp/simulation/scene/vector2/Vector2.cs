using System;
using System.Numerics;

namespace ModularRobot.Vector
{
    /// <summary>
    /// Represents a 2D vector.
    /// </summary>
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x = 0.0f, float y = 0.0f)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Subtracts one vector from another.
        /// </summary>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        public static Vector2 operator *(Vector2 a, float scalar)
        {
            return new Vector2(a.X * scalar, a.Y * scalar);
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Returns the negated version of the vector.
        /// </summary>
        public Vector2 Inverse => new Vector2(-X, -Y);

        /// <summary>
        /// Compares two vectors for equality.
        /// </summary>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// Compares two vectors for inequality.
        /// </summary>
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2 other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// Creates a new vector from a 3x3 matrix's translation component.
        /// </summary>
        public static Vector2 FromMatrix33Translation(Matrix3x2 matrix)
        {
            return new Vector2(matrix.M31, matrix.M32);
        }
    }
}
