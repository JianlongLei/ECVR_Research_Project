using System;
using System.Collections.Generic;
using ModularRobot.Scenes;
using ModularRobot.Simulation;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Utility to convert modular robot scenes into a batch of simulation scenes.
    /// </summary>
    public static class ToBatch
    {
        /// <summary>
        /// Converts one or more modular robot scenes to a batch of simulation scenes.
        /// </summary>
        /// <param name="scenes">The modular robot scene(s) to make the batch from.</param>
        /// <param name="batchParameters">Parameters for the batch that are not contained in the modular robot scenes.</param>
        /// <param name="recordSettings">Settings for recording the simulations, if any.</param>
        /// <returns>
        /// A tuple containing the created batch and a mapping from modular robots to multi-body systems for each scene.
        /// </returns>
        public static (Batch Batch, List<Dictionary<UUIDKey<ModularRobot>, MultiBodySystem>> Mapping)
            ConvertToBatch(
                IEnumerable<ModularRobotScene> scenes,
                BatchParameters batchParameters,
                RecordSettings? recordSettings = null)
        {
            if (scenes == null) throw new ArgumentNullException(nameof(scenes));
            if (batchParameters == null) throw new ArgumentNullException(nameof(batchParameters));

            // Prepare converted scenes and mappings
            var converted = new List<(Scene SimulationScene, Dictionary<UUIDKey<ModularRobot>, MultiBodySystem> Mapping)>();
            foreach (var modularRobotScene in scenes)
            {
                var result = modularRobotScene.ToSimulationScene();
                converted.Add(result);
            }

            // Create the batch
            var batch = new Batch(batchParameters) { RecordSettings = recordSettings };
            foreach (var (simulationScene, _) in converted)
            {
                batch.Scenes.Add(simulationScene);
            }

            // Extract mappings
            var mappings = new List<Dictionary<UUIDKey<ModularRobot>, MultiBodySystem>>();
            foreach (var (_, mapping) in converted)
            {
                mappings.Add(mapping);
            }

            return (batch, mappings);
        }
    }
}
