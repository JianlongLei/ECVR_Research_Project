namespace Revolve2.Database
{
    /// <summary>
    /// Describes the way a database should be opened.
    /// </summary>
    public enum OpenMethod
    {
        OpenIfExists,
        OpenOrCreate,
        NotExistsAndCreate,
        OverwriteIfExists
    }
}
