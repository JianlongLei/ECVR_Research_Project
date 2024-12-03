using System;
using System.Collections.Generic;
using System.Linq;

namespace Revolve2.Robot
{
    /// <summary>
    /// Represents the structure of a CPG network, including CPGs and their connections.
    /// </summary>
    public class CpgNetworkStructure
    {
        public List<Cpg> Cpgs { get; }
        public HashSet<CpgPair> Connections { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CpgNetworkStructure"/> class.
        /// </summary>
        /// <param name="cpgs">The list of CPGs in the network.</param>
        /// <param name="connections">The set of connections between CPGs.</param>
        public CpgNetworkStructure(List<Cpg> cpgs, HashSet<CpgPair> connections)
        {
            Cpgs = cpgs ?? throw new ArgumentNullException(nameof(cpgs));
            Connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        /// <summary>
        /// Creates a specified number of CPGs.
        /// </summary>
        /// <param name="count">The number of CPGs to create.</param>
        /// <returns>A list of created CPGs.</returns>
        public static List<Cpg> MakeCpgs(int count)
        {
            var cpgs = new List<Cpg>();
            for (int i = 0; i < count; i++)
            {
                cpgs.Add(new Cpg { Index = i, OutputIndex = i });
            }
            return cpgs;
        }

        /// <summary>
        /// Generates the connection weights matrix for the network.
        /// </summary>
        /// <param name="internalWeights">A dictionary of internal weights for each CPG.</param>
        /// <param name="externalWeights">A dictionary of external weights for each connection pair.</param>
        /// <returns>The connection weights matrix as a 2D array.</returns>
        public float[,] MakeConnectionWeightsMatrix(
            Dictionary<Cpg, float> internalWeights,
            Dictionary<CpgPair, float> externalWeights)
        {
            int n = Cpgs.Count;
            var matrix = new float[n, n];

            foreach (var cpg in Cpgs)
            {
                matrix[cpg.Index, cpg.Index] = internalWeights[cpg];
            }

            foreach (var pair in Connections)
            {
                float weight = externalWeights[pair];
                matrix[pair.CpgIndexLowest.Index, pair.CpgIndexHighest.Index] = weight;
                matrix[pair.CpgIndexHighest.Index, pair.CpgIndexLowest.Index] = weight;
            }

            return matrix;
        }

        /// <summary>
        /// Creates a uniform initial state for the CPG network.
        /// </summary>
        /// <param name="value">The uniform value for each state entry.</param>
        /// <returns>An array representing the uniform initial state.</returns>
        public float[] MakeUniformState(float value)
        {
            return Enumerable.Repeat(value, Cpgs.Count).ToArray();
        }
    }

    /// <summary>
    /// Represents a pair of connected CPGs.
    /// </summary>
    public class CpgPair
    {
        public Cpg CpgIndexLowest { get; }
        public Cpg CpgIndexHighest { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CpgPair"/> class.
        /// Ensures the lowest index is assigned to <see cref="CpgIndexLowest"/>.
        /// </summary>
        /// <param name="cpg1">The first CPG in the pair.</param>
        /// <param name="cpg2">The second CPG in the pair.</param>
        public CpgPair(Cpg cpg1, Cpg cpg2)
        {
            if (cpg1.Index <= cpg2.Index)
            {
                CpgIndexLowest = cpg1;
                CpgIndexHighest = cpg2;
            }
            else
            {
                CpgIndexLowest = cpg2;
                CpgIndexHighest = cpg1;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not CpgPair other) return false;
            return CpgIndexLowest == other.CpgIndexLowest && CpgIndexHighest == other.CpgIndexHighest;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CpgIndexLowest, CpgIndexHighest);
        }
    }

    /// <summary>
    /// Represents a single CPG in the network.
    /// </summary>
    public class Cpg
    {
        public int Index { get; set; }
        public int OutputIndex { get; set; }
    }
}
