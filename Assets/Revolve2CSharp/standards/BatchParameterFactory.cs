namespace Revolve2.Simulation
{
    /// <summary>
    /// Factory for creating standard batch parameters.
    /// </summary>
    public static class BatchParameterFactory
    {
        public const int SimulationTime = 30;
        public const float SamplingFrequency = 5.0f;
        public const float SimulationTimestep = 0.001f;
        public const float ControlFrequency = 20.0f;
        /// <summary>
        /// Creates batch parameters with standard values.
        /// </summary>
        /// <param name="simulationTime">Simulation time in seconds.</param>
        /// <param name="samplingFrequency">Frequency for data sampling.</param>
        /// <param name="simulationTimestep">Timestep for the simulation.</param>
        /// <param name="controlFrequency">Control frequency for the simulation.</param>
        /// <returns>A <see cref="BatchParameters"/> object with the specified or standard values.</returns>
        public static BatchParameters MakeStandardBatchParameters(
            int simulationTime = SimulationTime,
            float? samplingFrequency = SamplingFrequency,
            float simulationTimestep = SimulationTimestep,
            float controlFrequency = ControlFrequency)
        {
            return new BatchParameters(simulationTime, samplingFrequency, simulationTimestep, controlFrequency);
        }
    }
}
