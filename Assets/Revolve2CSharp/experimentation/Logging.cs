using System;
using System.IO;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Provides logging utilities for experiments.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// Sets up logging with a specified level and optional log file.
        /// </summary>
        /// <param name="level">The minimum log level to capture.</param>
        /// <param name="fileName">Optional log file name to write to.</param>
        public static void SetupLogging(LogLevel level = LogLevel.Info, string fileName = null)
        {
            // Configure console logging
            Console.WriteLine("=======================================");
            Console.WriteLine("=======================================");
            Console.WriteLine("=======================================");
            Console.WriteLine("       New log starts here.            ");
            Console.WriteLine("=======================================");
            Console.WriteLine("=======================================");
            Console.WriteLine("=======================================");

            // If a log file is specified, write to the file
            if (!string.IsNullOrEmpty(fileName))
            {
                File.AppendAllText(fileName, "New log starts here.\n");
            }
        }

        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error,
            Critical
        }
    }
}
