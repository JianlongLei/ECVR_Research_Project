using System.Collections.Generic;

namespace Revolve2.Evolution
{
    /// <summary>
    /// Abstract class for evaluating individuals in an evolutionary process.
    /// </summary>
    public abstract class Evaluator<TPopulation>
    {
        /// <summary>
        /// Evaluates individuals from a population.
        /// </summary>
        /// <param name="population">The population for evaluation.</param>
        /// <returns>The results of the evaluation.</returns>
        public abstract List<float> Evaluate(TPopulation population);
    }
}
