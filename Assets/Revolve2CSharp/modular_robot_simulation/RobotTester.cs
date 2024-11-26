using System;
using ModularRobot.Scenes;
using ModularRobot.Simulation;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Utility to test a robot on a specified terrain using a simulator.
    /// </summary>
    public static class RobotTester
    {
        /// <summary>
        /// Tests a robot with a dummy brain or an existing modular robot configuration.
        /// </summary>
        /// <param name="robot">The modular robot or its body.</param>
        /// <param name="terrain">The terrain to test the robot on.</param>
        /// <param name="simulator">The simulator to run the test.</param>
        /// <param name="batchParameters">The parameters for the simulation batch.</param>
        public static void TestRobot(
            object robot,
            Terrain terrain,
            ISimulator simulator,
            BatchParameters batchParameters)
        {
            if (robot == null) throw new ArgumentNullException(nameof(robot));
            if (terrain == null) throw new ArgumentNullException(nameof(terrain));
            if (simulator == null) throw new ArgumentNullException(nameof(simulator));
            if (batchParameters == null) throw new ArgumentNullException(nameof(batchParameters));

            ModularRobot modularRobot;

            // Check if the input is a Body or a ModularRobot
            if (robot is Body body)
            {
                // Create a modular robot with a dummy brain
                var brain = new BrainDummy();
                modularRobot = new ModularRobot(body, brain);
            }
            else if (robot is ModularRobot existingRobot)
            {
                modularRobot = existingRobot;
            }
            else
            {
                throw new ArgumentException("The robot parameter must be of type Body or ModularRobot.", nameof(robot));
            }

            // Create the scene with the specified terrain
            var scene = new ModularRobotScene(terrain);
            scene.AddRobot(modularRobot);

            // Simulate the scene
            SceneSimulator.SimulateScene(
                simulator: simulator,
                batchParameters: batchParameters,
                scene: scene
            );
        }
    }
}
