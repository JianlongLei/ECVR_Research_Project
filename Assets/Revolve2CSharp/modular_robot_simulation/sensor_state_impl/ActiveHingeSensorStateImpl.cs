using System;
using ModularRobot.Joints;
using ModularRobot.Sensors;
using ModularRobot.Simulation;

namespace ModularRobot.SensorStates
{
    /// <summary>
    /// Implements the active hinge sensor state.
    /// </summary>
    public class ActiveHingeSensorStateImpl : ActiveHingeSensorState
    {
        private readonly SimulationState _simulationState;
        private readonly JointHinge _hingeJoint;

        public ActiveHingeSensorStateImpl(SimulationState simulationState, JointHinge hingeJoint)
        {
            _simulationState = simulationState ?? throw new ArgumentNullException(nameof(simulationState));
            _hingeJoint = hingeJoint ?? throw new ArgumentNullException(nameof(hingeJoint));
        }

        public override float Position => _simulationState.GetHingeJointPosition(_hingeJoint);
    }
}
