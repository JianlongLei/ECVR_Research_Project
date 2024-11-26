using System;
using System.Collections.Generic;
using ModularRobot.Simulation;
using ModularRobot.Mapping;
using ModularRobot.Utilities;
using ModularRobot.Joints;

namespace ModularRobot.Builders
{
    /// <summary>
    /// A builder for active hinge sensors.
    /// </summary>
    public class ActiveHingeSensorBuilder : Builder
    {
        private readonly ActiveHingeSensor _sensor;
        private readonly RigidBody _rigidBody;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveHingeSensorBuilder"/> class.
        /// </summary>
        /// <param name="sensor">The active hinge sensor to be built.</param>
        /// <param name="rigidBody">The rigid body to attach the sensor to.</param>
        public ActiveHingeSensorBuilder(ActiveHingeSensor sensor, RigidBody rigidBody)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
            _rigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
        }

        /// <inheritdoc/>
        public override List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            if (multiBodySystem == null) throw new ArgumentNullException(nameof(multiBodySystem));
            if (bodyToMultiBodySystemMapping == null) throw new ArgumentNullException(nameof(bodyToMultiBodySystemMapping));

            // Get the joint for the rigid body and map it
            var joint = multiBodySystem.GetJointsForRigidBody(_rigidBody)[0];
            bodyToMultiBodySystemMapping.ActiveHingeSensorToJointHinge[new UUIDKey<ActiveHingeSensor>(_sensor)] = (JointHinge)joint;

            return new List<UnbuiltChild>();
        }
    }
}
