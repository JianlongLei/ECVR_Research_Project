using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Evolution
{
    /// <summary>
    /// Implements steady-state selection for evolutionary algorithms.
    /// </summary>
    public static class SteadyStateSelection
    {
        /// <summary>
        /// Selects individuals for the next generation from both parents and offspring.
        /// </summary>
        /// <typeparam name="TGenotype">The type representing the genotype.</typeparam>
        /// <typeparam name="TFitness">The type representing the fitness values.</typeparam>
        /// <param name="oldGenotypes">Genotypes of individuals in the parent population.</param>
        /// <param name="oldFitnesses">Fitnesses of individuals in the parent population.</param>
        /// <param name="newGenotypes">Genotypes of individuals from the offspring.</param>
        /// <param name="newFitnesses">Fitnesses of individuals from the offspring.</param>
        /// <param name="selectionFunction">Function to select n individuals from a population based on genotype and fitness.</param>
        /// <returns>
        /// A tuple containing indices of selected individuals from the parent population
        /// and indices of selected individuals from the offspring.
        /// </returns>
        public static (List<int> SelectedOld, List<int> SelectedNew) Select<TGenotype, TFitness>(
            List<TGenotype> oldGenotypes,
            List<TFitness> oldFitnesses,
            List<TGenotype> newGenotypes,
            List<TFitness> newFitnesses,
            Func<int, List<TGenotype>, List<TFitness>, List<int>> selectionFunction)
        {
            if (oldGenotypes == null || oldFitnesses == null || newGenotypes == null || newFitnesses == null)
                throw new ArgumentNullException("One or more input lists are null.");
            if (oldGenotypes.Count != oldFitnesses.Count)
                throw new ArgumentException("Old genotypes and fitnesses must have the same length.");
            if (newGenotypes.Count != newFitnesses.Count)
                throw new ArgumentException("New genotypes and fitnesses must have the same length.");

            int populationSize = oldGenotypes.Count;

            // Combine old and new populations
            var combinedGenotypes = oldGenotypes.Concat(newGenotypes).ToList();
            var combinedFitnesses = oldFitnesses.Concat(newFitnesses).ToList();

            // Perform selection
            var selection = selectionFunction(populationSize, combinedGenotypes, combinedFitnesses);

            // Separate selected indices into old and new categories
            var selectedOld = selection.Where(i => i < oldFitnesses.Count).ToList();
            var selectedNew = selection.Where(i => i >= oldFitnesses.Count)
                .Select(i => i - oldFitnesses.Count).ToList();

            return (selectedOld, selectedNew);
        }
    }
}
