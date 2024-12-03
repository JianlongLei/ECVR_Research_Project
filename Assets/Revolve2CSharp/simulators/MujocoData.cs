using System;
using System.Collections.Generic;

namespace Revolve2.Simulation.Mujoco
{
    /// <summary>
    /// Information about a MuJoCo hinge joint.
    /// </summary>
    public class JointHingeMujoco
    {
        public int Id { get; set; }
        public int CtrlIndexPosition { get; set; }
        public int CtrlIndexVelocity { get; set; }

        public JointHingeMujoco(int id, int ctrlIndexPosition, int ctrlIndexVelocity)
        {
            Id = id;
            CtrlIndexPosition = ctrlIndexPosition;
            CtrlIndexVelocity = ctrlIndexVelocity;
        }
    }

    /// <summary>
    /// Information about a MuJoCo body.
    /// </summary>
    public class MultiBodySystemMujoco
    {
        public int Id { get; set; }

        public MultiBodySystemMujoco(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Information about a MuJoCo IMU sensor.
    /// </summary>
    public class IMUSensorMujoco
    {
        public int GyroId { get; set; }
        public int AccelerometerId { get; set; }

        public IMUSensorMujoco(int gyroId, int accelerometerId)
        {
            GyroId = gyroId;
            AccelerometerId = accelerometerId;
        }
    }

    /// <summary>
    /// Information about a MuJoCo camera sensor.
    /// </summary>
    public class CameraSensorMujoco
    {
        public int CameraId { get; set; }
        public Tuple<int, int> CameraSize { get; set; }

        public CameraSensorMujoco(int cameraId, Tuple<int, int> cameraSize)
        {
            CameraId = cameraId;
            CameraSize = cameraSize;
        }
    }
}
