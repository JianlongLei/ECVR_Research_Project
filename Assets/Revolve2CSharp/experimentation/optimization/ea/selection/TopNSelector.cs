using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Provides functionality to select the top N elements based on fitness.
    /// </summary>
    public static class TopNSelector
    {
        public static List<int> GetTopN<TFitness>(
            int n,
            List<TFitness> fitnesses)
            where TFitness : IComparable<TFitness>
        {
            if (fitnesses.Count < n)
                throw new ArgumentException("Cannot select more elements than available.");

            return Sorting.ArgSort(fitnesses).TakeLast(n).Reverse().ToList();
        }
    }
}
