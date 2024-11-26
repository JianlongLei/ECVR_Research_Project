using System;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Represents texture reference information for simulators.
    /// </summary>
    public abstract class TextureReference
    {
        public string? Builtin { get; set; }
        public string? File { get; set; }
        public string? ContentType { get; set; }
        public string? GridLayout { get; set; }

        protected TextureReference(string? builtin = null, string? file = null, string? contentType = null, string? gridLayout = null)
        {
            if (builtin == null && file == null)
                throw new ArgumentException("No texture reference provided in the form of a file or builtin texture.");

            if (file != null && contentType == null)
                throw new ArgumentException("Please specify the content type for your texture source file.");

            Builtin = builtin;
            File = file;
            ContentType = contentType;
            GridLayout = gridLayout;
        }
    }
}
