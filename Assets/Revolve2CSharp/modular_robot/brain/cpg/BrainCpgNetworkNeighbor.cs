using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Robot
{
    /// <summary>
    /// A CPG brain with active hinges that are connected if they are within 2 jumps in the modular robot's tree structure.
    /// </summary>
    public abstract class BrainCpgNetworkNeighbor : Brain
    {
        private float[] _initialState;
        private float[,] _weightMatrix;
        private List<(int StateIndex, ActiveHinge)> _outputMapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrainCpgNetworkNeighbor"/> class.
        /// </summary>
        /// <param name="body">The body of the modular robot to create the brain for.</param>
        protected BrainCpgNetworkNeighbor(Body body)
        {
            var activeHinges = body.FindModulesOfType<ActiveHinge>();

            var cpgNetworkStructure = CpgNetworkStructureNeighborGenerator
                .ActiveHingesToCpgNetworkStructureNeighbor(activeHinges, out _outputMapping);

            var connections = cpgNetworkStructure.Connections
                .Select(pair => (
                    activeHinges[pair.CpgIndexLowest.Index],
                    activeHinges[pair.CpgIndexHighest.Index]))
                .ToList();

            var (internalWeights, externalWeights) = MakeWeights(activeHinges, connections, body);

            _weightMatrix = cpgNetworkStructure.MakeConnectionWeightsMatrix(
                cpgNetworkStructure.Cpgs.Zip(internalWeights, (cpg, weight) => (cpg, weight))
                    .ToDictionary(x => x.cpg, x => x.weight),
                cpgNetworkStructure.Connections.Zip(externalWeights, (pair, weight) => (pair, weight))
                    .ToDictionary(x => x.pair, x => x.weight)
            );

            _initialState = cpgNetworkStructure.MakeUniformState(0.5f * MathF.Sqrt(2));
        }

        /// <summary>
        /// Creates an instance of the brain.
        /// </summary>
        public override BrainInstance MakeInstance()
        {
            return new BrainCpgInstance(_initialState, _weightMatrix, _outputMapping);
        }

        /// <summary>
        /// Defines the weights between neurons.
        /// </summary>
        protected abstract (List<float> InternalWeights, List<float> ExternalWeights)
            MakeWeights(List<ActiveHinge> activeHinges, List<(ActiveHinge, ActiveHinge)> connections, Body body);
    }
}
