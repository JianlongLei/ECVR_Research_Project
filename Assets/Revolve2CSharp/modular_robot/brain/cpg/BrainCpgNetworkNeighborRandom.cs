using System;
using System.Collections.Generic;

namespace Revolve2.Robot
{
    /// <summary>
    /// A CPG brain with random weights between neurons.
    /// </summary>
    public class BrainCpgNetworkNeighborRandom : BrainCpgNetworkNeighbor
    {
        private readonly Random _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrainCpgNetworkNeighborRandom"/> class.
        /// </summary>
        /// <param name="body">The body of the modular robot to create the brain for.</param>
        /// <param name="rng">The random number generator used for generating the weights.</param>
        public BrainCpgNetworkNeighborRandom(Body body, Random rng)
            : base(body)
        {
            _rng = rng ?? throw new ArgumentNullException(nameof(rng));
        }

        /// <summary>
        /// Defines the weights between neurons using random values.
        /// </summary>
        protected override (List<float> InternalWeights, List<float> ExternalWeights)
            MakeWeights(List<ActiveHinge> activeHinges, List<(ActiveHinge, ActiveHinge)> connections, Body body)
        {
            var internalWeights = new List<float>(activeHinges.Count);
            var externalWeights = new List<float>(connections.Count);

            for (int i = 0; i < activeHinges.Count; i++)
                internalWeights.Add((float)(_rng.NextDouble() * 2.0 - 1.0));

            for (int i = 0; i < connections.Count; i++)
                externalWeights.Add((float)(_rng.NextDouble() * 2.0 - 1.0));

            return (internalWeights, externalWeights);
        }
    }
}
