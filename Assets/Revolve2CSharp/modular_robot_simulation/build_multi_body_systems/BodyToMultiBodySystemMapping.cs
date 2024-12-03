using System.Collections.Generic;
using Revolve2.Joints;
using Revolve2.Robot;
using Revolve2.Simulation;
using Revolve2.Utilities;
using Sensors = Revolve2.Sensors;

namespace Revolve2.Mapping
{
    /// <summary>
    /// Maps modular robot components to multi-body system objects.
    /// </summary>
    public class BodyToMultiBodySystemMapping
    {
        public MultiBodySystem MultiBodySystem { get; }

        public Dictionary<UUIDKey<ActiveHinge>, JointHinge> ActiveHingeToJointHinge { get; }
        public Dictionary<UUIDKey<ActiveHingeSensor>, JointHinge> ActiveHingeSensorToJointHinge { get; }
        public Dictionary<UUIDKey<IMUSensor>, Sensors.IMUSensor> IMUToSimIMU { get; }
        public Dictionary<UUIDKey<CameraSensor>, Sensors.CameraSensor> CameraToSimCamera { get; }

        public BodyToMultiBodySystemMapping(MultiBodySystem multiBodySystem)
        {
            MultiBodySystem = multiBodySystem;
            ActiveHingeToJointHinge = new Dictionary<UUIDKey<ActiveHinge>, JointHinge>();
            ActiveHingeSensorToJointHinge = new Dictionary<UUIDKey<ActiveHingeSensor>, JointHinge>();
            IMUToSimIMU = new Dictionary<UUIDKey<IMUSensor>, Sensors.IMUSensor>();
            CameraToSimCamera = new Dictionary<UUIDKey<CameraSensor>, Sensors.CameraSensor>();
        }
    }
}
