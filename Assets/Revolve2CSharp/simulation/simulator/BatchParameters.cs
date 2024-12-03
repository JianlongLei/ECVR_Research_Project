namespace Revolve2.Simulation
{
    /// <summary>
    /// Parameters for a simulation batch.
    /// </summary>s
    public class BatchParameters
    {
        /// <summary>
        /// Seconds. The duration for which each robot should be simulated. If null, the simulation will run indefinitely.
        /// </summary>
        public float? SimulationTime { get; set; }

        /// <summary>
        /// Hz. Frequency for state sampling during the simulation. If null, no sampling will be performed.
        /// </summary>
        public float? SamplingFrequency { get; set; }

        /// <summary>
        /// Simulation time step in seconds.
        /// </summary>
        public float SimulationTimestep { get; set; }

        /// <summary>
        /// Frequency for how often the control function is called.
        /// </summary>
        public float ControlFrequency { get; set; }

        public BatchParameters(float? simulationTime, float? samplingFrequency, float simulationTimestep, float controlFrequency)
        {
            SimulationTime = simulationTime;
            SamplingFrequency = samplingFrequency;
            SimulationTimestep = simulationTimestep;
            ControlFrequency = controlFrequency;
        }
    }
}
