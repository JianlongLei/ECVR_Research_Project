using System;
using System.Collections.Generic;
using Revolve2.Joints;
using Revolve2.Sensors;
using Revolve2.Simulation;
using Revolve2.Utilities;

namespace Revolve2.Simulation.Mujoco
{
    /// <summary>
    /// Data to interpret a MuJoCo model using the simulation abstraction.
    /// </summary>
    public class AbstractionToMujocoMapping
    {
        public Dictionary<UUIDKey<JointHinge>, JointHingeMujoco> HingeJoints { get; } =
            new Dictionary<UUIDKey<JointHinge>, JointHingeMujoco>();

        public Dictionary<UUIDKey<MultiBodySystem>, MultiBodySystemMujoco> MultiBodySystems { get; } =
            new Dictionary<UUIDKey<MultiBodySystem>, MultiBodySystemMujoco>();

        public Dictionary<UUIDKey<IMUSensor>, IMUSensorMujoco> ImuSensors { get; } =
            new Dictionary<UUIDKey<IMUSensor>, IMUSensorMujoco>();

        public Dictionary<UUIDKey<CameraSensor>, CameraSensorMujoco> CameraSensors { get; } =
            new Dictionary<UUIDKey<CameraSensor>, CameraSensorMujoco>();
    }
}
