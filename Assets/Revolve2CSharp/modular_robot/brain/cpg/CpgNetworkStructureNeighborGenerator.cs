using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Robot
{
    /// <summary>
    /// Provides functionality to generate a CPG network structure based on neighboring active hinges.
    /// </summary>
    public static class CpgNetworkStructureNeighborGenerator
    {
        /// <summary>
        /// Converts active hinges into a CPG network structure where connections are based on proximity within two jumps.
        /// </summary>
        /// <param name="activeHinges">The list of active hinges in the robot.</param>
        /// <param name="outputMapping">Output mapping of state indices to active hinges.</param>
        /// <returns>A CPG network structure.</returns>
        public static CpgNetworkStructure ActiveHingesToCpgNetworkStructureNeighbor(
            List<ActiveHinge> activeHinges,
            out List<(int StateIndex, ActiveHinge)> outputMapping)
        {
            if (activeHinges == null)
            {
                throw new ArgumentNullException(nameof(activeHinges));
            }

            // Create CPGs corresponding to active hinges.
            var cpgs = CpgNetworkStructure.MakeCpgs(activeHinges.Count);

            // Map active hinges to CPGs for quick lookup.
            var activeHingeToCpg = activeHinges.Zip(cpgs, (hinge, cpg) => (hinge, cpg))
                                               .ToDictionary(x => x.hinge, x => x.cpg);

            // Generate connections based on proximity.
            var connections = new HashSet<CpgPair>();
            for (int i = 0; i < activeHinges.Count; i++)
            {
                var hinge = activeHinges[i];
                var cpg = cpgs[i];

                // Find neighbors within two jumps and connect them.
                var neighbors = hinge.Neighbours(2).OfType<ActiveHinge>();
                foreach (var neighbor in neighbors)
                {
                    if (activeHingeToCpg.TryGetValue(neighbor, out var neighborCpg))
                    {
                        connections.Add(new CpgPair(cpg, neighborCpg));
                    }
                }
            }


            // Create the output mapping of state indices to hinges.
            outputMapping = cpgs.Zip(activeHinges, (cpg, hinge) => (cpg.OutputIndex, hinge)).ToList();

            // Return the generated CPG network structure.
            return new CpgNetworkStructure(cpgs, connections);
        }
    }
}
