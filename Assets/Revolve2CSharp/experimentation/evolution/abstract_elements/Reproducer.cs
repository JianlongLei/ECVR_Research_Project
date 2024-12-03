using System.Collections.Generic;

namespace Revolve2.Evolution
{
    /// <summary>
    /// Abstract class for enabling reproduction in an evolutionary process.
    /// </summary>
    public abstract class Reproducer<TPopulation>
    {
        /// <summary>
        /// Produces offspring from a population.
        /// </summary>
        /// <param name="population">The population of parents.</param>
        /// <param name="kwargs">Additional parameters for reproduction.</param>
        /// <returns>The offspring population.</returns>
        public abstract TPopulation Reproduce(TPopulation population, Dictionary<string, object> kwargs = null);
    }
}
