using Revolve2.Robot;
using Revolve2.Scenes;
using Revolve2.Utilities;
using Revolve2.TerrainGeneration;
using Revolve2.Vector;
using Revolve2.Simulation;

namespace Revolve2Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Run the simulation
            RunSimulation();
        }

        private static void RunSimulation()
        {
            // Set up logging (assumed to be a static method in your logging framework)
            // Logging.SetupLogging();

            // Initialize a random number generator
            var rng = RandomNumberGenerator.CreateTimeSeedGenerator();

            // Create the robot's body
            var body = CreateBody();

            // Create the robot's brain using a CPG brain with random parameters
            var brain = new BrainCpgNetworkNeighborRandom(body, rng);

            // Combine the body and brain into a ModularRobot
            var robot = new ModularRobot(body, brain);

            // Create a flat terrain
            var terrain = TerrainFactory.CreateFlatTerrain(new Vector2(20.0f, 20.0f));

            // Create a modular robot scene and add the robot
            var scene = new ModularRobotScene(terrain);
            scene.AddRobot(robot);

            // Add an interactive object (a ball)
            // scene.AddInteractiveObject(new Ball(0.1f, 0.1f, new Pose(new Vector3(-0.5f, 0.5f, 0))));

            // Create a simulator using the Mujoco backend
            // var simulator = new LocalSimulator(ViewerType.Native);

            // // Define simulation parameters
            // var batchParameters = BatchParameterFactory.MakeStandardBatchParameters();
            // batchParameters.SimulationTime = 60f;

            // // Simulate the scene
            // SceneSimulator.SimulateScenes(
            //     simulator: simulator,
            //     batchParameters: batchParameters,
            //     scenes: new List<ModularRobotScene> { scene }
            // );

        }

        private static BodyV2 CreateBody()
        {
            // Create the body structure
            var body = new BodyV2();

            // Configure the left face of the core
            ActiveHingeV2 leftBottom = new ActiveHingeV2(RightAngles.Deg0);
            ActiveHingeV2 leftBottomAttachment = new ActiveHingeV2(RightAngles.Deg0);
            leftBottom.Attachment = leftBottomAttachment;
            BrickV2 leftBottomAttachmentAttachment = new BrickV2(RightAngles.Deg0);
            leftBottomAttachment.Attachment = leftBottomAttachmentAttachment;
            body.CoreV2.LeftFace.Bottom = leftBottom;

            // Configure the right face of the core
            ActiveHingeV2 rightBottom = new ActiveHingeV2(RightAngles.Deg0);
            ActiveHingeV2 rightBottomAttachment = new ActiveHingeV2(RightAngles.Deg0);
            rightBottom.Attachment = rightBottomAttachment;
            BrickV2 rightBottomAttachmentAttachment = new BrickV2(RightAngles.Deg0);
            rightBottomAttachment.Attachment = rightBottomAttachmentAttachment;
            body.CoreV2.RightFace.Bottom = rightBottom;

            return body;
        }
    }
}
