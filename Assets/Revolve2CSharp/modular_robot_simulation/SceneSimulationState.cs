using System;
using System.Collections.Generic;
using ModularRobot.Simulation;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Represents a specific state of a modular robot simulation within a scene.
    /// </summary>
    public class SceneSimulationState
    {
        private readonly SimulationState _simulationState;
        private readonly Dictionary<UUIDKey<ModularRobot>, MultiBodySystem> _modularRobotToMultiBodySystemMapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneSimulationState"/> class.
        /// </summary>
        /// <param name="simulationState">The simulation state for the scene.</param>
        /// <param name="modularRobotToMultiBodySystemMapping">Mapping of modular robots to their multi-body systems.</param>
        public SceneSimulationState(
            SimulationState simulationState,
            Dictionary<UUIDKey<ModularRobot>, MultiBodySystem> modularRobotToMultiBodySystemMapping)
        {
            _simulationState = simulationState ?? throw new ArgumentNullException(nameof(simulationState));
            _modularRobotToMultiBodySystemMapping = modularRobotToMultiBodySystemMapping ?? throw new ArgumentNullException(nameof(modularRobotToMultiBodySystemMapping));
        }

        /// <summary>
        /// Retrieves the simulation state for a specific modular robot in the scene.
        /// </summary>
        /// <param name="modularRobot">The modular robot to retrieve the state for.</param>
        /// <returns>The simulation state of the specified modular robot.</returns>
        /// <exception cref="ArgumentException">Thrown if the modular robot is not in the scene.</exception>
        public ModularRobotSimulationState GetModularRobotSimulationState(ModularRobot modularRobot)
        {
            if (modularRobot == null) throw new ArgumentNullException(nameof(modularRobot));

            if (!_modularRobotToMultiBodySystemMapping.TryGetValue(new UUIDKey<ModularRobot>(modularRobot), out var multiBodySystem))
            {
                throw new ArgumentException("Modular robot not in scene.", nameof(modularRobot));
            }

            return new ModularRobotSimulationState(_simulationState, multiBodySystem);
        }
    }
}
