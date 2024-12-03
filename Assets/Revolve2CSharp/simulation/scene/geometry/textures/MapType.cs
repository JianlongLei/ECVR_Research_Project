namespace Revolve2.Utilities
{
    /// <summary>
    /// Enumerates different map types for textures.
    /// </summary>
    public enum MapType
    {
        /// <summary>
        /// Maps the texture to a 2D surface.
        /// </summary>
        MAP2D,

        /// <summary>
        /// Wraps the texture around a cube object.
        /// </summary>
        CUBE,

        /// <summary>
        /// Maps the texture onto the inside of an object, like a skybox.
        /// </summary>
        SKYBOX
    }
}
