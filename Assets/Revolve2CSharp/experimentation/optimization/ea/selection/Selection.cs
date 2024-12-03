using System;
using System.Collections.Generic;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Selects multiple distinct individuals from a population using a provided selection function.
    /// </summary>
    public class Selection
    {
        public List<int> MultipleUnique<TIndividual, TFitness>(
            int selectionSize,
            List<TIndividual> population,
            List<TFitness> fitnesses,
            Func<List<TIndividual>, List<TFitness>, int> selectionFunction)
        {
            if (population.Count != fitnesses.Count)
                throw new ArgumentException("Population and fitnesses must have the same length.");
            if (selectionSize > population.Count)
                throw new ArgumentException("Selection size cannot exceed the population size.");

            var selectedIndices = new HashSet<int>();

            while (selectedIndices.Count < selectionSize)
            {
                int selectedIndex = selectionFunction(population, fitnesses);
                selectedIndices.Add(selectedIndex);
            }

            return new List<int>(selectedIndices);
        }
    }
}
