using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Robot
{
    /// <summary>
    /// Represents a static CPG network brain where weights and states are predefined.
    /// </summary>
    public class BrainCpgNetworkStatic : BrainCpgNetworkNeighbor
    {
        private readonly List<float> _internalWeights;
        private readonly List<float> _externalWeights;
        private readonly List<float> _uniformState;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrainCpgNetworkStatic"/> class.
        /// </summary>
        /// <param name="body">The body of the modular robot to create the brain for.</param>
        /// <param name="internalWeights">The predefined internal weights for the CPGs.</param>
        /// <param name="externalWeights">The predefined external weights between CPG connections.</param>
        /// <param name="uniformState">The initial state of the CPG network.</param>
        public BrainCpgNetworkStatic(
            Body body,
            List<float> internalWeights,
            List<float> externalWeights,
            List<float> uniformState)
            : base(body)
        {
            _internalWeights = internalWeights ?? throw new ArgumentNullException(nameof(internalWeights));
            _externalWeights = externalWeights ?? throw new ArgumentNullException(nameof(externalWeights));
            _uniformState = uniformState ?? throw new ArgumentNullException(nameof(uniformState));
        }

        /// <summary>
        /// Creates a static brain with uniform weights and state initialization.
        /// </summary>
        /// <param name="body">The body of the modular robot to create the brain for.</param>
        /// <param name="internalWeight">The uniform internal weight value.</param>
        /// <param name="externalWeight">The uniform external weight value.</param>
        /// <param name="stateValue">The uniform state value.</param>
        /// <returns>A new instance of <see cref="BrainCpgNetworkStatic"/>.</returns>
        public static BrainCpgNetworkStatic UniformFromParams(
            Body body,
            float internalWeight,
            float externalWeight,
            float stateValue)
        {
            int activeHingesCount = body.FindModulesOfType<ActiveHinge>().Count;
            int connectionsCount = (int)Math.Ceiling(activeHingesCount * 1.5); // Approximation for connections.

            var internalWeights = Enumerable.Repeat(internalWeight, activeHingesCount).ToList();
            var externalWeights = Enumerable.Repeat(externalWeight, connectionsCount).ToList();
            var uniformState = Enumerable.Repeat(stateValue, activeHingesCount).ToList();

            return new BrainCpgNetworkStatic(body, internalWeights, externalWeights, uniformState);
        }

        /// <summary>
        /// Defines the weights between neurons.
        /// </summary>
        /// <param name="activeHinges">The active hinges corresponding to each CPG.</param>
        /// <param name="connections">Pairs of active hinges corresponding to connected CPGs.</param>
        /// <param name="body">The body of the robot.</param>
        /// <returns>A tuple containing two lists: internal weights and external weights.</returns>
        protected override (List<float> InternalWeights, List<float> ExternalWeights) MakeWeights(
            List<ActiveHinge> activeHinges,
            List<(ActiveHinge, ActiveHinge)> connections,
            Body body)
        {
            return (_internalWeights, _externalWeights);
        }

        /// <summary>
        /// Creates a uniform initial state for the CPG network.
        /// </summary>
        /// <param name="value">The uniform value for the initial state.</param>
        /// <param name="count">The number of CPGs in the network.</param>
        /// <returns>A list representing the uniform initial state.</returns>
        public static List<float> CreateUniformState(float value, int count)
        {
            return Enumerable.Repeat(value, count).ToList();
        }
    }
}
