using System;
using System.Collections.Generic;
using ModularRobot.Simulation;
using ModularRobot.Mapping;
using ModularRobot.Utilities;
using System.Numerics;

namespace ModularRobot.Builders
{
    /// <summary>
    /// A builder for attachment faces.
    /// </summary>
    public class AttachmentFaceBuilder : Builder
    {
        private readonly AttachmentFace _module;
        private readonly RigidBody _rigidBody;
        private readonly Pose _slotPose;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentFaceBuilder"/> class.
        /// </summary>
        /// <param name="module">The attachment face module to be built.</param>
        /// <param name="rigidBody">The rigid body to attach the face to.</param>
        /// <param name="slotPose">The pose of the slot.</param>
        public AttachmentFaceBuilder(AttachmentFace module, RigidBody rigidBody, Pose slotPose)
        {
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _rigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
            _slotPose = slotPose ?? throw new ArgumentNullException(nameof(slotPose));
        }

        /// <inheritdoc/>
        public override List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            if (multiBodySystem == null) throw new ArgumentNullException(nameof(multiBodySystem));
            if (bodyToMultiBodySystemMapping == null) throw new ArgumentNullException(nameof(bodyToMultiBodySystemMapping));

            var tasks = new List<UnbuiltChild>();

            // Iterate through attachment points
            foreach (var attachmentPoint in _module.AttachmentPoints)
            {
                var child = _module.Children.GetValueOrDefault(attachmentPoint.Key);
                if (child != null)
                {
                    var unbuiltChild = new UnbuiltChild(child, _rigidBody);

                    // Update the pose for the child
                    unbuiltChild.UpdatePose(
                        position: _slotPose.Position + Vector3.Transform(attachmentPoint.Value.Offset, _slotPose.Orientation),
                        orientation: _slotPose.Orientation * attachmentPoint.Value.Orientation
                    );

                    tasks.Add(unbuiltChild);
                }
            }

            return tasks;
        }
    }
}
