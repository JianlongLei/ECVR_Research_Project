using System;
using System.Collections.Generic;
using ModularRobot.Mapping;
using ModularRobot.Simulation;
using ModularRobot.Utilities;

namespace ModularRobot.Builders
{
    public class CoreBuilder : Builder
    {
        private readonly Core _module;
        private readonly RigidBody _rigidBody;
        private readonly Pose _slotPose;

        public CoreBuilder(Core module, RigidBody rigidBody, Pose slotPose)
        {
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _rigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
            _slotPose = slotPose ?? throw new ArgumentNullException(nameof(slotPose));
        }

        public override List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            var tasks = new List<UnbuiltChild>();

            // Build the core geometry
            // Add children and sensors to tasks

            return tasks;
        }
    }
}
