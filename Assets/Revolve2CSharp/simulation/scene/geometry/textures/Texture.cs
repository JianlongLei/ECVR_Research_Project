using System;
using ModularRobot.Utilities;

namespace ModularRobot.Geo
{
    /// <summary>
    /// Represents an abstract texture for geometric models.
    /// </summary>
    public class Texture
    {
        public TextureReference? Reference { get; set; }
        public Color BaseColor { get; set; }
        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }
        public MapType MapType { get; set; }
        public (int, int) Repeat { get; set; }
        public (int, int) Size { get; set; }
        public float Specular { get; set; }
        public float Shininess { get; set; }
        public float Reflectance { get; set; }
        public float Emission { get; set; }

        public Texture(
            TextureReference? reference = null,
            Color? baseColor = null,
            Color? primaryColor = null,
            Color? secondaryColor = null,
            MapType mapType = MapType.CUBE,
            (int, int)? repeat = null,
            (int, int)? size = null,
            float specular = 0.5f,
            float shininess = 0.5f,
            float reflectance = 0.0f,
            float emission = 0.0f)
        {
            Reference = reference;
            BaseColor = baseColor ?? new Color(255, 255, 255, 255);
            PrimaryColor = primaryColor ?? new Color(0, 0, 0, 0);
            SecondaryColor = secondaryColor ?? new Color(0, 0, 0, 0);
            MapType = mapType;
            Repeat = repeat ?? (1, 1);
            Size = size ?? (100, 100);
            Specular = Math.Clamp(specular, 0f, 1f);
            Shininess = Math.Clamp(shininess, 0f, 1f);
            Reflectance = Math.Clamp(reflectance, 0f, 1f);
            Emission = Math.Max(0f, emission);
        }
    }
}
