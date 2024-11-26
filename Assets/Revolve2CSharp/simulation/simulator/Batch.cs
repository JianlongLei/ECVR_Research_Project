using System;
using System.Collections.Generic;

namespace ModularRobot.Simulation
{
    /// <summary>
    /// Represents a set of scenes and shared parameters for simulation.
    /// </summary>
    public class Batch
    {
        /// <summary>
        /// Parameters for the simulation batch.
        /// </summary>
        public BatchParameters Parameters { get; }

        /// <summary>
        /// The scenes to simulate.
        /// </summary>
        public List<Scene> Scenes { get; } = new();

        /// <summary>
        /// Settings for recording the simulation, if any.
        /// </summary>
        public RecordSettings? RecordSettings { get; set; }

        public Batch(BatchParameters parameters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }
    }
}
