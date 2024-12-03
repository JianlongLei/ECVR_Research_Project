using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Provides methods for Pareto frontier calculations.
    /// </summary>
    public static class ParetoFrontier
    {
        public static List<int> GetParetoFrontier(
            List<List<double>> frontierValues,
            List<bool> frontierOrder,
            int toTake)
        {
            if (frontierValues.Any(v => v.Count != frontierValues[0].Count))
                throw new ArgumentException("All value lists must have the same length.");
            if (frontierValues[0].Count < toTake)
                throw new ArgumentException("Cannot take more elements than available in the frontier.");

            var dominationOrders = GetDominationOrders(
                frontierValues.Select(values => values.ToArray()).ToArray(),
                frontierOrder
            );
            // Convert dominationOrders into a List<List<double>>
            var dominationOrderValues = dominationOrders
                .Select(d => new List<double> { d }) // Wrap each `double` in a List<double>
                .ToList();

            // Combine dominationOrderValues with frontierValues
            var allValues = dominationOrderValues
                .Concat(frontierValues)
                .ToList();

            var sortedIndices = Enumerable.Range(0, dominationOrders.Length)
                .OrderByDescending(i => allValues[0][i])
                .ThenBy(i => allValues.Skip(1).Select(a => a[i]).ToArray())
                .Take(toTake)
                .ToList();

            return sortedIndices;
        }

        private static int[] GetDominationOrders(double[][] valueArray, List<bool> frontierOrder)
        {
            var dominationOrders = new int[valueArray.Length];

            for (int i = 0; i < valueArray.Length; i++)
            {
                dominationOrders[i] = valueArray
                    .Count(p => p.Zip(valueArray[i], (x, y) =>
                        frontierOrder.Zip(p, (asc, pv) =>
                            asc ? pv < y : pv > y).All(b => b)).Any());
            }

            return dominationOrders;
        }
    }
}
