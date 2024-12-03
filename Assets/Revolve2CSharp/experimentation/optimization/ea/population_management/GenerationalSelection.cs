using System;
using System.Collections.Generic;

namespace Revolve2.Evolution
{
    /// <summary>
    /// Implements generational selection for evolutionary algorithms.
    /// </summary>
    public static class GenerationalSelection
    {
        /// <summary>
        /// Selects individuals exclusively from offspring for the next generation.
        /// </summary>
        /// <typeparam name="TGenotype">The type representing the genotype.</typeparam>
        /// <typeparam name="TFitness">The type representing the fitness values.</typeparam>
        /// <param name="oldGenotypes">Genotypes of individuals in the parent population.</param>
        /// <param name="oldFitnesses">Fitnesses of individuals in the parent population.</param>
        /// <param name="newGenotypes">Genotypes of individuals from the offspring.</param>
        /// <param name="newFitnesses">Fitnesses of individuals from the offspring.</param>
        /// <param name="selectionFunction">Function to select n individuals from a population based on genotype and fitness.</param>
        /// <returns>
        /// A tuple containing an empty list of indices for selected old individuals
        /// and a list of indices for selected individuals from the offspring.
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
            if (newGenotypes.Count != newFitnesses.Count)
                throw new ArgumentException("New genotypes and fitnesses must have the same length.");
            if (newFitnesses.Count < oldGenotypes.Count)
                throw new ArgumentException("The size of new individuals must be at least the size of the old population.");

            int populationSize = oldGenotypes.Count;
            var selectedNew = selectionFunction(populationSize, newGenotypes, newFitnesses);

            return (new List<int>(), selectedNew);
        }
    }
}
