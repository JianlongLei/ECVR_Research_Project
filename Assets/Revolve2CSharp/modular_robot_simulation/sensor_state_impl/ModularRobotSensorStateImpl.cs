using System;
using Revolve2.Mapping;
using Revolve2.Robot;
using Revolve2.Sensors;
using Revolve2.Simulation;
using Revolve2.Utilities;
using Sensors = Revolve2.Sensors;

namespace Revolve2.SensorStates
{
    /// <summary>
    /// Implements the modular robot sensor state.
    /// </summary>
    public class ModularRobotSensorStateImpl : ModularRobotSensorState
    {
        private readonly SimulationState _simulationState;
        private readonly BodyToMultiBodySystemMapping _bodyToMultiBodySystemMapping;

        public ModularRobotSensorStateImpl(SimulationState simulationState, BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            _simulationState = simulationState ?? throw new ArgumentNullException(nameof(simulationState));
            _bodyToMultiBodySystemMapping = bodyToMultiBodySystemMapping ?? throw new ArgumentNullException(nameof(bodyToMultiBodySystemMapping));
        }

        public override ActiveHingeSensorState GetActiveHingeSensorState(ActiveHingeSensor sensor)
        {
            if (sensor == null) throw new ArgumentNullException(nameof(sensor));

            if (!_bodyToMultiBodySystemMapping.ActiveHingeSensorToJointHinge.TryGetValue(new UUIDKey<ActiveHingeSensor>(sensor), out var jointHinge))
            {
                throw new InvalidOperationException("Sensor not in scene.");
            }

            return new ActiveHingeSensorStateImpl(_simulationState, jointHinge);
        }

        public override IMUSensorState GetIMUSensorState(Robot.IMUSensor sensor)
        {
            if (sensor == null) throw new ArgumentNullException(nameof(sensor));

            if (!_bodyToMultiBodySystemMapping.IMUToSimIMU.TryGetValue(new UUIDKey<Robot.IMUSensor>(sensor), out var imuSensor))
            {
                throw new InvalidOperationException("IMU not in scene.");
            }

            return new IMUSensorStateImpl(_simulationState, _bodyToMultiBodySystemMapping.MultiBodySystem, imuSensor);
        }

        public override CameraSensorState GetCameraSensorState(Robot.CameraSensor sensor)
        {
            if (sensor == null) throw new ArgumentNullException(nameof(sensor));

            if (!_bodyToMultiBodySystemMapping.CameraToSimCamera.TryGetValue(new UUIDKey<Robot.CameraSensor>(sensor), out var cameraSensor))
            {
                throw new InvalidOperationException("Camera not in scene.");
            }

            return new CameraSensorStateImpl(_simulationState, _bodyToMultiBodySystemMapping.MultiBodySystem, cameraSensor);
        }
    }
}
