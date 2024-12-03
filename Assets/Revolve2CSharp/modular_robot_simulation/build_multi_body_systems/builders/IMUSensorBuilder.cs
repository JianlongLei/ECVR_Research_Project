using System;
using System.Collections.Generic;
using System.Numerics;
using Revolve2.Mapping;
using Revolve2.Robot;
using Revolve2.Simulation;
using Revolve2.Utilities;
using Sensors = Revolve2.Sensors;

namespace Revolve2.Builders
{
    public class IMUSensorBuilder : Builder
    {
        private readonly IMUSensor _sensor;
        private readonly RigidBody _rigidBody;
        private readonly Pose _pose;
        private readonly Vector3 _imuLocation;

        public IMUSensorBuilder(IMUSensor sensor, RigidBody rigidBody, Pose pose, Vector3 imuLocation)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
            _rigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
            _pose = pose ?? throw new ArgumentNullException(nameof(pose));
            _imuLocation = imuLocation;
        }

        public override List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            // Add the IMU sensor to the multi-body system
            var imuPose = new Pose(_pose.Position + _imuLocation, _pose.Orientation);
            var imuSensor = new Sensors.IMUSensor(_pose);
            bodyToMultiBodySystemMapping.IMUToSimIMU[new UUIDKey<IMUSensor>(_sensor)] = imuSensor;
            _rigidBody.Sensors.AddSensor(imuSensor);

            return new List<UnbuiltChild>();
        }
    }
}
