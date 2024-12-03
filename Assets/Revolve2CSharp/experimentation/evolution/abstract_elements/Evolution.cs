using System.Collections.Generic;

namespace Revolve2.Evolution
{
    /// <summary>
    /// Abstract class encapsulating an evolutionary process.
    /// </summary>
    public abstract class Evolution<TPopulation>
    {
        /// <summary>
        /// Advances the evolutionary process by one iteration.
        /// </summary>
        /// <param name="population">The current population.</param>
        /// <param name="kwargs">Additional parameters for the evolutionary step.</param>
        /// <returns>The resulting population after one evolutionary step.</returns>
        public abstract TPopulation Step(TPopulation population, Dictionary<string, object> kwargs = null);
    }
}
