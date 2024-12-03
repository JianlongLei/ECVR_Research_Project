using System.Collections.Generic;
using Revolve2.Robot;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Represents a simulation scene containing multi-body systems.
    /// </summary>
    public class Scene
    {
        public SimulationHandler Handler { get; }
        private readonly List<MultiBodySystem> _multiBodySystems;

        public Scene(SimulationHandler handler)
        {
            Handler = handler;
            _multiBodySystems = new List<MultiBodySystem>();
        }

        public void AddMultiBodySystem(MultiBodySystem multiBodySystem)
        {
            _multiBodySystems.Add(multiBodySystem);
        }

        public IReadOnlyList<MultiBodySystem> MultiBodySystems => _multiBodySystems.AsReadOnly();
    }
}
