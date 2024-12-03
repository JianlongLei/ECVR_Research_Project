using System.Collections.Generic;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Interface for a simulator.
    /// </summary>
    public interface ISimulator
    {
        /// <summary>
        /// Simulate the provided batch by simulating each contained scene.
        /// </summary>
        /// <param name="batch">The batch to run.</param>
        /// <returns>List of simulation states in ascending order of time.</returns>
        List<List<SimulationState>> SimulateBatch(Batch batch);
    }
}
