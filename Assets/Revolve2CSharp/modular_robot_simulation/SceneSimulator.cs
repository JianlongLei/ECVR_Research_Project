using System;
using System.Collections.Generic;
using Revolve2.Scenes;
using Revolve2.Simulation;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Utility to simulate one or more modular robot scenes.
    /// </summary>
    public static class SceneSimulator
    {
        /// <summary>
        /// Simulates a single scene.
        /// </summary>
        /// <param name="simulator">The simulator to use for the simulation.</param>
        /// <param name="batchParameters">The batch parameters for the simulation.</param>
        /// <param name="scene">The scene to simulate.</param>
        /// <param name="recordSettings">Optional record settings for the simulation.</param>
        /// <returns>A list of simulation states for the scene.</returns>
        public static List<SceneSimulationState> SimulateScene(
            ISimulator simulator,
            BatchParameters batchParameters,
            ModularRobotScene scene,
            RecordSettings? recordSettings = null)
        {
            return SimulateScenes(simulator, batchParameters, new List<ModularRobotScene> { scene }, recordSettings)[0];
        }

        /// <summary>
        /// Simulates multiple scenes.
        /// </summary>
        /// <param name="simulator">The simulator to use for the simulation.</param>
        /// <param name="batchParameters">The batch parameters for the simulation.</param>
        /// <param name="scenes">The scenes to simulate.</param>
        /// <param name="recordSettings">Optional record settings for the simulation.</param>
        /// <returns>A list of lists of simulation states, one for each scene.</returns>
        public static List<List<SceneSimulationState>> SimulateScenes(
            ISimulator simulator,
            BatchParameters batchParameters,
            List<ModularRobotScene> scenes,
            RecordSettings? recordSettings = null)
        {
            if (simulator == null) throw new ArgumentNullException(nameof(simulator));
            if (batchParameters == null) throw new ArgumentNullException(nameof(batchParameters));
            if (scenes == null || scenes.Count == 0) throw new ArgumentException("Scenes cannot be null or empty.", nameof(scenes));

            // Convert scenes to batch
            var (batch, modularRobotToMultiBodySystemMappings) = ToBatch.ConvertToBatch(scenes, batchParameters, recordSettings);

            // Simulate the batch
            var simulationResults = simulator.SimulateBatch(batch);

            // Convert results to SceneSimulationState
            var results = new List<List<SceneSimulationState>>();
            for (int i = 0; i < simulationResults.Count; i++)
            {
                var sceneStates = new List<SceneSimulationState>();
                var simulationStates = simulationResults[i];
                var mapping = modularRobotToMultiBodySystemMappings[i];

                foreach (var state in simulationStates)
                {
                    sceneStates.Add(new SceneSimulationState(state, mapping));
                }

                results.Add(sceneStates);
            }

            return results;
        }
    }
}
