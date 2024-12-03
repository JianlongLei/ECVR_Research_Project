using System;
using System.Collections.Generic;
using Revolve2.Mapping;
using Revolve2.Robot;
using Revolve2.Simulation;
using Revolve2.Utilities;
using CamSim = Revolve2.Sensors;

namespace Revolve2.Builders
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
            CamSim.CameraSensor sensor = new CamSim.CameraSensor(_pose, _sensor.CameraSize);
            bodyToMultiBodySystemMapping.CameraToSimCamera[new UUIDKey<CameraSensor>(_sensor)] = sensor;
            _rigidBody.Sensors.AddSensor(sensor);

            return new List<UnbuiltChild>();
        }
    }
}
