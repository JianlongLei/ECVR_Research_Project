namespace ModularRobot.Rendering
{
    /// <summary>
    /// Interface for rendering simulations.
    /// </summary>
    public interface IViewer
    {
        /// <summary>
        /// Close the viewer.
        /// </summary>
        void CloseViewer();

        /// <summary>
        /// Render the scene on the viewer.
        /// </summary>
        /// <returns>Optional feedback.</returns>
        object? Render();

        /// <summary>
        /// Get the current viewport size.
        /// </summary>
        /// <returns>The size as a tuple.</returns>
        (int Width, int Height) CurrentViewportSize();

        /// <summary>
        /// Gets the viewport object.
        /// </summary>
        object ViewPort { get; }

        /// <summary>
        /// Returns the viewer context.
        /// </summary>
        object Context { get; }

        /// <summary>
        /// Checks if the viewer can record.
        /// </summary>
        bool CanRecord { get; }
    }
}
