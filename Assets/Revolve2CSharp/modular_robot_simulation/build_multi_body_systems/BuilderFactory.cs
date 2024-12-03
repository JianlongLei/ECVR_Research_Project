using System;
using System.Collections.Generic;
using Revolve2.Builders;
using Revolve2.Robot;
using Revolve2.Utilities;

namespace Revolve2.Factories
{
    /// <summary>
    /// Factory to get and initialize the corresponding builder for a given module.
    /// </summary>
    public static class BuilderFactory
    {
        /// <summary>
        /// Gets the builder corresponding to the specified unbuilt child.
        /// </summary>
        /// <param name="unbuiltChild">The unbuilt child to get the builder for.</param>
        /// <returns>The initialized builder for the unbuilt child.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no builder is available for the given module type.</exception>
        public static Builder GetBuilder(UnbuiltChild unbuiltChild)
        {
            if (unbuiltChild == null) throw new ArgumentNullException(nameof(unbuiltChild));

            var childObject = unbuiltChild.ChildObject;

            return childObject switch
            {
                Core core => new CoreBuilder(core, unbuiltChild.RigidBody, unbuiltChild.Pose),
                Brick brick => new BrickBuilder(brick, unbuiltChild.RigidBody, unbuiltChild.Pose),
                ActiveHinge hinge => new ActiveHingeBuilder(hinge, unbuiltChild.RigidBody, unbuiltChild.Pose),
                AttachmentFace attachmentFace => new AttachmentFaceBuilder(attachmentFace, unbuiltChild.RigidBody, unbuiltChild.Pose),
                ActiveHingeSensor hingeSensor => new ActiveHingeSensorBuilder(hingeSensor, unbuiltChild.RigidBody),
                IMUSensor imuSensor => new IMUSensorBuilder(imuSensor, unbuiltChild.RigidBody, unbuiltChild.Pose, imuSensor.Position),
                CameraSensor cameraSensor => new CameraSensorBuilder(cameraSensor, unbuiltChild.RigidBody, unbuiltChild.Pose),
                _ => throw new KeyNotFoundException($"No builder defined for module type: {childObject.GetType().Name}.")
            };
        }
    }
}
