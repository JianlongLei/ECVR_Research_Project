using System;
using ModularRobot.Simulation;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Represents the state of a modular robot at a specific moment in a simulation.
    /// </summary>
    public class ModularRobotSimulationState
    {
        private readonly SimulationState _simulationState;
        private readonly MultiBodySystem _multiBodySystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularRobotSimulationState"/> class.
        /// </summary>
        /// <param name="simulationState">The simulation state corresponding to this modular robot state.</param>
        /// <param name="multiBodySystem">The multi-body system this modular robot corresponds to.</param>
        public ModularRobotSimulationState(SimulationState simulationState, MultiBodySystem multiBodySystem)
        {
            _simulationState = simulationState ?? throw new ArgumentNullException(nameof(simulationState));
            _multiBodySystem = multiBodySystem ?? throw new ArgumentNullException(nameof(multiBodySystem));
        }

        /// <summary>
        /// Gets the pose of the modular robot.
        /// </summary>
        /// <returns>The pose of the modular robot.</returns>
        public Pose GetPose()
        {
            return _simulationState.GetMultiBodySystemPose(_multiBodySystem);
        }

        /// <summary>
        /// Gets the pose of a module, relative to its parent module's reference frame.
        /// </summary>
        /// <param name="module">The module to get the pose for.</param>
        /// <returns>The relative pose.</returns>
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public Pose GetModuleRelativePose(Module module)
        {
            throw new NotImplementedException("The method is not implemented.");
        }

        /// <summary>
        /// Gets the pose of a module, relative to the global reference frame.
        /// </summary>
        /// <param name="module">The module to get the pose for.</param>
        /// <returns>The absolute pose.</returns>
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public Pose GetModuleAbsolutePose(Module module)
        {
            throw new NotImplementedException("The method is not implemented.");
        }
    }
}
