using System;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Interface for types that support the less-than operator.
    /// </summary>
    public interface ISupportsLt<T> where T : IComparable<T>
    {
        bool LessThan(T other);
    }
}
