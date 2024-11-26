using System;
using System.Collections.Generic;
using ModularRobot.Control;
using ModularRobot.Mapping;
using ModularRobot.SensorStates;
using ModularRobot.Simulation;

namespace ModularRobot.Handlers
{
    /// <summary>
    /// Implements the simulation handler for a modular robot scene.
    /// </summary>
    public class ModularRobotSimulationHandler : SimulationHandler
    {
        private readonly List<(BrainInstance BrainInstance, BodyToMultiBodySystemMapping Mapping)> _brains;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularRobotSimulationHandler"/> class.
        /// </summary>
        public ModularRobotSimulationHandler()
        {
            _brains = new List<(BrainInstance, BodyToMultiBodySystemMapping)>();
        }

        /// <summary>
        /// Adds a brain instance that will control a robot during the simulation.
        /// </summary>
        /// <param name="brainInstance">The brain instance.</param>
        /// <param name="bodyToMultiBodySystemMapping">A mapping from the robot body to the multi-body system.</param>
        public void AddRobot(BrainInstance brainInstance, BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            if (brainInstance == null) throw new ArgumentNullException(nameof(brainInstance));
            if (bodyToMultiBodySystemMapping == null) throw new ArgumentNullException(nameof(bodyToMultiBodySystemMapping));

            _brains.Add((brainInstance, bodyToMultiBodySystemMapping));
        }

        /// <summary>
        /// Handles a simulation frame by updating all robots in the scene.
        /// </summary>
        /// <param name="simulationState">The current state of the simulation.</param>
        /// <param name="simulationControl">Interface for setting control targets.</param>
        /// <param name="dt">The time elapsed since the last simulation frame.</param>
        public override void Handle(SimulationState simulationState, ControlInterface simulationControl, float dt)
        {
            if (simulationState == null) throw new ArgumentNullException(nameof(simulationState));
            if (simulationControl == null) throw new ArgumentNullException(nameof(simulationControl));
            if (dt <= 0) throw new ArgumentOutOfRangeException(nameof(dt), "Time delta must be greater than zero.");

            foreach (var (brainInstance, bodyToMultiBodySystemMapping) in _brains)
            {
                // Create sensor and control interfaces for the robot
                var sensorState = new ModularRobotSensorStateImpl(simulationState, bodyToMultiBodySystemMapping);
                var controlInterface = new ModularRobotControlInterfaceImpl(simulationControl, bodyToMultiBodySystemMapping);

                // Delegate control to the robot's brain
                brainInstance.Control(dt, sensorState, controlInterface);
            }
        }
    }
}
