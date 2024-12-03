namespace Revolve2.Evolution
{
    /// <summary>
    /// Abstract class for enabling learning in an evolutionary process.
    /// </summary>
    public abstract class Learner<TPopulation>
    {
        protected Evaluator<TPopulation> RewardFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Learner{TPopulation}"/> class.
        /// </summary>
        /// <param name="rewardFunction">The evaluator used as a reward function.</param>
        protected Learner(Evaluator<TPopulation> rewardFunction)
        {
            RewardFunction = rewardFunction;
        }

        /// <summary>
        /// Enables individuals in a population to learn.
        /// </summary>
        /// <param name="population">The population.</param>
        /// <returns>The learned population.</returns>
        public abstract TPopulation Learn(TPopulation population);
    }
}
