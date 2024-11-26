using System;
using System.Collections.Generic;
using ModularRobot.Mapping;
using ModularRobot.Simulation;
using ModularRobot.Utilities;
using CamSim = ModularRobot.Sensors;

namespace ModularRobot.Builders
{
    public class CameraSensorBuilder : Builder
    {
        private readonly CameraSensor _sensor;
        private readonly RigidBody _rigidBody;
        private readonly Pose _pose;

        public CameraSensorBuilder(CameraSensor sensor, RigidBody rigidBody, Pose pose)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
            _rigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
            _pose = pose ?? throw new ArgumentNullException(nameof(pose));
        }

        public override List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            // Add the camera sensor to the multi-body system
            CameraSensor sensor = new CameraSensor(_pose.Position, _pose.Orientation, _sensor.CameraSize);
            bodyToMultiBodySystemMapping.CameraToSimCamera[new UUIDKey<CameraSensor>(_sensor)] = sensor;
            _rigidBody.Sensors.AddSensor(sensor);

            return new List<UnbuiltChild>();
        }
    }
}
