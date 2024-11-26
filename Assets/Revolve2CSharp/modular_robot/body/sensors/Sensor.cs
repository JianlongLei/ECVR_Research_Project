using System;
using System.Numerics;

namespace ModularRobot
{
    public abstract class Sensor:IOrientation
    {
        private Guid _uuid;
        private Quaternion _orientation;
        private Vector3 _position;

        public Guid Uuid => _uuid;
        public Quaternion Orientation => _orientation;
        public Vector3 Position => _position;

        protected Sensor(Quaternion orientation, Vector3 position)
        {
            _orientation = orientation;
            _uuid = Guid.NewGuid();
            _position = position;
        }
    }
}
