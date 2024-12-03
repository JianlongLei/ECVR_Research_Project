using System.Collections.Generic;

namespace Revolve2.Evolution
{
    /// <summary>
    /// Abstract class for selecting individuals in an evolutionary process.
    /// </summary>
    public abstract class Selector<TPopulation>
    {
        /// <summary>
        /// Selects individuals from a population.
        /// </summary>
        /// <param name="population">The population for selection.</param>
        /// <param name="kwargs">Additional metrics or parameters for selection.</param>
        /// <returns>A tuple containing the selected subset of the population and additional parameters.</returns>
        public abstract (TPopulation Selected, Dictionary<string, object> AdditionalParams) Select(
            TPopulation population, 
            Dictionary<string, object> kwargs = null);
    }
}
