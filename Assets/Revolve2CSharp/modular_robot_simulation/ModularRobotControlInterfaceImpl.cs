using System;
using Revolve2.Mapping;
using Revolve2.Simulation;
using Revolve2.Utilities;
using Revolve2;
using Revolve2.Robot;

namespace Revolve2.Control
{
    /// <summary>
    /// Implementation of the <see cref="IModularRobotControlInterface"/>.
    /// </summary>
    public class ModularRobotControlInterfaceImpl : IModularRobotControlInterface
    {
        private readonly ControlInterface _simulationControl;
        private readonly BodyToMultiBodySystemMapping _bodyToMultiBodySystemMapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularRobotControlInterfaceImpl"/> class.
        /// </summary>
        /// <param name="simulationControl">Control interface of the actual simulation.</param>
        /// <param name="bodyToMultiBodySystemMapping">Mapping from body to multi-body system.</param>
        public ModularRobotControlInterfaceImpl(
            ControlInterface simulationControl,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            _simulationControl = simulationControl ?? throw new ArgumentNullException(nameof(simulationControl));
            _bodyToMultiBodySystemMapping = bodyToMultiBodySystemMapping ?? throw new ArgumentNullException(nameof(bodyToMultiBodySystemMapping));
        }


        /// <summary>
        /// Sets the position target for an active hinge.
        /// </summary>
        /// <param name="activeHinge">The active hinge to set the target for.</param>
        /// <param name="target">The target position to set.</param>
        public void SetActiveHingeTarget(ActiveHinge activeHinge, float target)
        {
            if (activeHinge == null) throw new ArgumentNullException(nameof(activeHinge));

            // Clamp the target to the hinge's range
            float clampedTarget = Math.Clamp(target, -activeHinge.Range, activeHinge.Range);

            // Map the active hinge to the corresponding joint and set the target
            _simulationControl.SetJointHingePositionTarget(
                _bodyToMultiBodySystemMapping.ActiveHingeToJointHinge[new UUIDKey<ActiveHinge>(activeHinge)],
                clampedTarget);
        
        }

    }
}
