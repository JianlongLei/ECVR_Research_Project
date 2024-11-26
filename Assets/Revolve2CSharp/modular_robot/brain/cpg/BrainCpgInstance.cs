using System;
using System.Collections.Generic;

namespace ModularRobot
{
    /// <summary>
    /// Represents a CPG brain instance that controls modular robots.
    /// </summary>
    public class BrainCpgInstance : BrainInstance
    {
        private float[] _state;
        private float[,] _weightMatrix;
        private List<(int StateIndex, ActiveHinge Hinge)> _outputMapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrainCpgInstance"/> class.
        /// </summary>
        /// <param name="initialState">The initial state of the neural network.</param>
        /// <param name="weightMatrix">The weight matrix used for integration.</param>
        /// <param name="outputMapping">Maps neurons to active hinges for control outputs.</param>
        public BrainCpgInstance(
            float[] initialState,
            float[,] weightMatrix,
            List<(int StateIndex, ActiveHinge Hinge)> outputMapping)
        {
            if (initialState.Length != weightMatrix.GetLength(0) ||
                weightMatrix.GetLength(0) != weightMatrix.GetLength(1))
            {
                throw new ArgumentException("Initial state and weight matrix dimensions must match.");
            }

            _state = initialState;
            _weightMatrix = weightMatrix;
            _outputMapping = outputMapping;
        }

        private static float[] RK45(float[] state, float[,] weightMatrix, float dt)
        {
            int n = state.Length;
            float[] A1 = MultiplyMatrixVector(weightMatrix, state);
            float[] A2 = MultiplyMatrixVector(weightMatrix, AddVectors(state, ScaleVector(A1, dt / 2)));
            float[] A3 = MultiplyMatrixVector(weightMatrix, AddVectors(state, ScaleVector(A2, dt / 2)));
            float[] A4 = MultiplyMatrixVector(weightMatrix, AddVectors(state, ScaleVector(A3, dt)));

            float[] newState = AddVectors(
                state,
                ScaleVector(AddVectors(A1, AddVectors(ScaleVector(AddVectors(A2, A3), 2), A4)), dt / 6)
            );

            return ClipVector(newState, -1.0f, 1.0f);
        }

        private static float[] MultiplyMatrixVector(float[,] matrix, float[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[] result = new float[rows];

            for (int i = 0; i < rows; i++)
            {
                result[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }

        private static float[] AddVectors(float[] a, float[] b)
        {
            if (a.Length != b.Length) throw new ArgumentException("Vectors must be the same length.");
            float[] result = new float[a.Length];
            for (int i = 0; i < a.Length; i++) result[i] = a[i] + b[i];
            return result;
        }

        private static float[] ScaleVector(float[] vector, float scalar)
        {
            float[] result = new float[vector.Length];
            for (int i = 0; i < vector.Length; i++) result[i] = vector[i] * scalar;
            return result;
        }

        private static float[] ClipVector(float[] vector, float min, float max)
        {
            float[] result = new float[vector.Length];
            for (int i = 0; i < vector.Length; i++) result[i] = Math.Clamp(vector[i], min, max);
            return result;
        }

        /// <summary>
        /// Controls the modular robot by updating hinge targets.
        /// </summary>
        /// <param name="dt">Elapsed time since the last call (in seconds).</param>
        /// <param name="sensorState">Current sensor states of the robot.</param>
        /// <param name="controlInterface">Interface to send control commands to the robot.</param>
        public override void Control(float dt, ModularRobotSensorState sensorState, IModularRobotControlInterface controlInterface)
        {
            _state = RK45(_state, _weightMatrix, dt);

            foreach (var (stateIndex, activeHinge) in _outputMapping)
            {
                controlInterface.SetActiveHingeTarget(activeHinge, _state[stateIndex] * activeHinge.Range);
            }
        }
    }
}
