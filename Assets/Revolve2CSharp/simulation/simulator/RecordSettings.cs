using System;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Settings for recording a simulation.
    /// </summary>
    public class RecordSettings
    {
        /// <summary>
        /// Directory for saving recorded videos.
        /// </summary>
        public string VideoDirectory { get; set; }

        /// <summary>
        /// If true, overwrite existing files.
        /// </summary>
        public bool Overwrite { get; set; }

        /// <summary>
        /// Frames per second for the recording.
        /// </summary>
        public int Fps { get; set; }

        /// <summary>
        /// Width of the recording, if specified.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Height of the recording, if specified.
        /// </summary>
        public int? Height { get; set; }

        public RecordSettings(string videoDirectory, bool overwrite = false, int fps = 24, int? width = null, int? height = null)
        {
            VideoDirectory = videoDirectory ?? throw new ArgumentNullException(nameof(videoDirectory));
            Overwrite = overwrite;
            Fps = fps;
            Width = width;
            Height = height;
        }
    }
}
