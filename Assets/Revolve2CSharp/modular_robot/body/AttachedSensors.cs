using System;
using System.Collections.Generic;

namespace ModularRobot
{
    public class AttachedSensors
    {
        private CameraSensor? _cameraSensor;
        private ActiveHingeSensor? _activeHingeSensor;
        private IMUSensor? _imuSensor;

        public CameraSensor? CameraSensor => _cameraSensor;
        public ActiveHingeSensor? ActiveHingeSensor => _activeHingeSensor;
        public IMUSensor? IMUSensor => _imuSensor;

        public AttachedSensors()
        {
            _cameraSensor = null;
            _activeHingeSensor = null;
            _imuSensor = null;
        }

        public void AddSensor(Sensor sensor)
        {
            switch (sensor)
            {
                case CameraSensor camera:
                    if (_cameraSensor == null)
                        _cameraSensor = camera;
                    else
                        throw new KeyNotFoundException("Camera sensor already populated.");
                    break;

                case IMUSensor imu:
                    if (_imuSensor == null)
                        _imuSensor = imu;
                    else
                        throw new KeyNotFoundException("IMU sensor already populated.");
                    break;

                case ActiveHingeSensor activeHinge:
                    if (_activeHingeSensor == null)
                        _activeHingeSensor = activeHinge;
                    else
                        throw new KeyNotFoundException("ActiveHinge sensor already populated.");
                    break;

                default:
                    throw new KeyNotFoundException($"Sensor type {sensor.GetType()} is not recognized.");
            }
        }

        public List<Sensor> GetAllSensors()
        {
            var sensors = new List<Sensor>();
            if (_activeHingeSensor != null) sensors.Add(_activeHingeSensor);
            if (_imuSensor != null) sensors.Add(_imuSensor);
            if (_cameraSensor != null) sensors.Add(_cameraSensor);
            return sensors;
        }
    }
}
