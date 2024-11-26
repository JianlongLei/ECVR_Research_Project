using System.Collections.Generic;
using ModularRobot.Joints;
using ModularRobot.Simulation;
using ModularRobot.Utilities;

namespace ModularRobot.Mapping
{
    /// <summary>
    /// Maps modular robot components to multi-body system objects.
    /// </summary>
    public class BodyToMultiBodySystemMapping
    {
        public MultiBodySystem MultiBodySystem { get; }

        public Dictionary<UUIDKey<ActiveHinge>, JointHinge> ActiveHingeToJointHinge { get; }
        public Dictionary<UUIDKey<ActiveHingeSensor>, JointHinge> ActiveHingeSensorToJointHinge { get; }
        public Dictionary<UUIDKey<IMUSensor>, IMUSensor> IMUToSimIMU { get; }
        public Dictionary<UUIDKey<CameraSensor>, CameraSensor> CameraToSimCamera { get; }

        public BodyToMultiBodySystemMapping(MultiBodySystem multiBodySystem)
        {
            MultiBodySystem = multiBodySystem;
            ActiveHingeToJointHinge = new Dictionary<UUIDKey<ActiveHinge>, JointHinge>();
            ActiveHingeSensorToJointHinge = new Dictionary<UUIDKey<ActiveHingeSensor>, JointHinge>();
            IMUToSimIMU = new Dictionary<UUIDKey<IMUSensor>, IMUSensor>();
            CameraToSimCamera = new Dictionary<UUIDKey<CameraSensor>, CameraSensor>();
        }
    }
}
