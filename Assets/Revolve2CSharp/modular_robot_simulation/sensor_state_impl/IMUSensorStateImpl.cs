using System;
using System.Numerics;
using ModularRobot.Sensors;
using ModularRobot.Simulation;

namespace ModularRobot.SensorStates
{
    /// <summary>
    /// Implements the simulation state for an IMU sensor.
    /// </summary>
    public class IMUSensorStateImpl : IMUSensorState
    {
        private readonly SimulationState _simulationState;
        private readonly MultiBodySystem _multiBodySystem;
        private readonly Vector3 _orientation;
        private readonly IMUSensor _imu;

        public IMUSensorStateImpl(SimulationState simulationState, MultiBodySystem multiBodySystem, IMUSensor imu)
        {
            _simulationState = simulationState ?? throw new ArgumentNullException(nameof(simulationState));
            _multiBodySystem = multiBodySystem ?? throw new ArgumentNullException(nameof(multiBodySystem));
            var ori = _simulationState.GetMultiBodySystemPose(_multiBodySystem).Orientation;
            _orientation = new Vector3(ori.X, ori.Y, ori.Z);
            _imu = imu ?? throw new ArgumentNullException(nameof(imu));
        }

        public override Vector3 SpecificForce => _simulationState.GetIMUSpecificForce(_imu);
        public override Vector3 AngularRate => _simulationState.GetIMUAngularRate(_imu);
        public override Vector3 Orientation => _orientation;

    }
}
