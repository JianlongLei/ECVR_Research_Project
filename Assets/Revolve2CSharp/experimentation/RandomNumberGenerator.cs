using System;
using System.Security.Cryptography;
using System.Text;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Provides utilities for random number generation.
    /// </summary>
    public static class RandomNumberGenerator
    {
        /// <summary>
        /// Creates a seed from the current time in microseconds.
        /// </summary>
        /// <param name="logSeed">Whether to log the generated seed.</param>
        /// <returns>The generated seed.</returns>
        public static int CreateTimeSeed(bool logSeed = true)
        {
            var seed = (int)(DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond * 1000);
            if (logSeed)
            {
                Console.WriteLine($"RNG seed: {seed}");
            }
            return seed;
        }

        /// <summary>
        /// Converts a string seed to an integer seed.
        /// </summary>
        /// <param name="text">The seed as a string.</param>
        /// <returns>The seed as an integer.</returns>
        public static int SeedFromString(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToInt32(hash, 0);
            }
        }

        /// <summary>
        /// Creates a random number generator using a seed.
        /// </summary>
        /// <param name="seed">The seed to use.</param>
        /// <returns>A random number generator.</returns>
        public static System.Random CreateGenerator(int seed)
        {
            return new System.Random(seed);
        }

        /// <summary>
        /// Creates a random number generator using a time-based seed.
        /// </summary>
        /// <param name="logSeed">Whether to log the automatically created seed.</param>
        /// <returns>A random number generator.</returns>
        public static System.Random CreateTimeSeedGenerator(bool logSeed = true)
        {
            return CreateGenerator(CreateTimeSeed(logSeed));
        }
    }
}
