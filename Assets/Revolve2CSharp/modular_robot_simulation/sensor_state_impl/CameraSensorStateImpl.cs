using System;
using System.Drawing;
using Revolve2.Sensors;
using Revolve2.Simulation;

namespace Revolve2.SensorStates
{
    /// <summary>
    /// Implements the simulation state for a camera sensor.
    /// </summary>
    public class CameraSensorStateImpl : CameraSensorState
    {
        private readonly SimulationState _simulationState;
        private readonly MultiBodySystem _multiBodySystem;
        private readonly CameraSensor _camera;

        public CameraSensorStateImpl(SimulationState simulationState, MultiBodySystem multiBodySystem, CameraSensor camera)
        {
            _simulationState = simulationState ?? throw new ArgumentNullException(nameof(simulationState));
            _multiBodySystem = multiBodySystem ?? throw new ArgumentNullException(nameof(multiBodySystem));
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        public override byte[,] Image => _simulationState.GetCameraView(_camera);
    }
}
