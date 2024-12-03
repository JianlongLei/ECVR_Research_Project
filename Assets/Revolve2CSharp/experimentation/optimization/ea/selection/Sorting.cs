using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Provides an implementation of argsort to get the indices of a sequence sorted by value.
    /// </summary>
    public static class Sorting
    {
        public static List<int> ArgSort<T>(IList<T> sequence) where T : IComparable<T>
        {
            return sequence
                .Select((value, index) => new { Value = value, Index = index })
                .OrderBy(item => item.Value)
                .Select(item => item.Index)
                .ToList();
        }
    }
}
