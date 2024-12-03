using Revolve2.Simulation;

namespace Revolve2.Robot
{
    /// <summary>
    /// Base class for handling a simulation, which includes, for example, controlling robots.
    /// </summary>
    public abstract class SimulationHandler
    {
        /// <summary>
        /// Handles a simulation frame.
        /// </summary>
        /// <param name="state">The current state of the simulation.</param>
        /// <param name="control">Interface for setting control targets.</param>
        /// <param name="dt">The time since the last call to this function.</param>
        public abstract void Handle(SimulationState state, ControlInterface control, float dt);
    }
}
