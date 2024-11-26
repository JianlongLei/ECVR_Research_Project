using System;

namespace ModularRobot.Sensors
{
    /// <summary>
    /// Base class for sensors in the modular robot framework.
    /// </summary>
    public abstract class Sensor
    {
        private readonly Guid _uuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sensor"/> class.
        /// </summary>
        protected Sensor()
        {
            _uuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the unique identifier for the sensor.
        /// </summary>
        public Guid UUID => _uuid;
    }
}
