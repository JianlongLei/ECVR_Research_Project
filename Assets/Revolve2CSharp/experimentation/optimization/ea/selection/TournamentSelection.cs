using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Implements tournament selection.
    /// </summary>
    public static class TournamentSelection
    {
        public static int Tournament<TFitness>(
            Random rng,
            List<TFitness> fitnesses,
            int k)
            where TFitness : IComparable<TFitness>
        {
            if (fitnesses.Count < k)
                throw new ArgumentException("Cannot select more participants than available.");

            var participantIndices = Enumerable.Range(0, fitnesses.Count)
                .OrderBy(_ => rng.Next())
                .Take(k)
                .ToList();

            return participantIndices
                .OrderByDescending(i => fitnesses[i])
                .First();
        }
    }
}
