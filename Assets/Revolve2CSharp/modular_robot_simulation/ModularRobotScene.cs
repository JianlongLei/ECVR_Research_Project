using System;
using System.Collections.Generic;
using Revolve2.Converters;
using Revolve2.Handlers;
using Revolve2.Robot;
using Revolve2.Simulation;
using Revolve2.Utilities;

namespace Revolve2.Scenes
{
    /// <summary>
    /// Represents a scene of modular robots on a terrain.
    /// </summary>
    public class ModularRobotScene
    {
        public Terrain Terrain { get; }
        private readonly List<(ModularRobot Robot, Pose Pose, bool TranslateZAABB)> _robots;
        private readonly List<MultiBodySystem> _interactiveObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularRobotScene"/> class.
        /// </summary>
        /// <param name="terrain">The terrain of the scene.</param>
        public ModularRobotScene(Terrain terrain)
        {
            Terrain = terrain ?? throw new ArgumentNullException(nameof(terrain));
            _robots = new List<(ModularRobot, Pose, bool)>();
            _interactiveObjects = new List<MultiBodySystem>();
        }

        /// <summary>
        /// Adds a robot to the scene.
        /// </summary>
        /// <param name="robot">The robot to add.</param>
        /// <param name="pose">The pose of the robot.</param>
        /// <param name="translateZAABB">Whether to translate the robot so it is placed on the ground.</param>
        public void AddRobot(ModularRobot robot, Pose pose = null, bool translateZAABB = true)
        {
            if (robot == null) throw new ArgumentNullException(nameof(robot));
            pose ??= new Pose();
            _robots.Add((robot, pose, translateZAABB));
        }

        /// <summary>
        /// Adds an interactive object to the scene.
        /// </summary>
        /// <param name="obj">The interactive object to add.</param>
        public void AddInteractiveObject(MultiBodySystem obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _interactiveObjects.Add(obj);
        }

        /// <summary>
        /// Converts this scene to a simulation scene.
        /// </summary>
        /// <returns>A tuple containing the created simulation scene and a mapping of modular robots to multi-body systems.</returns>
        public (Scene Scene, Dictionary<UUIDKey<ModularRobot>, MultiBodySystem> ModularRobotToMultiBodySystemMapping)
            ToSimulationScene()
        {
            var handler = new ModularRobotSimulationHandler();
            var scene = new Scene(handler);
            var modularRobotToMultiBodySystemMapping = new Dictionary<UUIDKey<ModularRobot>, MultiBodySystem>();

            // Add terrain to the simulation scene
            scene.AddMultiBodySystem(ConvertTerrain.Convert(Terrain));

            // Add robots to the simulation scene
            var converter = new BodyToMultiBodySystemConverter();
            foreach (var (robot, pose, translateZAABB) in _robots)
            {
                var (multiBodySystem, bodyToMultiBodySystemMapping) = converter.ConvertRobotBody(robot.Body, pose, translateZAABB);
                scene.AddMultiBodySystem(multiBodySystem);
                handler.AddRobot(robot.Brain.MakeInstance(), bodyToMultiBodySystemMapping);
                modularRobotToMultiBodySystemMapping.Add(new UUIDKey<ModularRobot>(robot), multiBodySystem);
            }

            // Add interactive objects to the simulation scene
            foreach (var interactiveObject in _interactiveObjects)
            {
                scene.AddMultiBodySystem(interactiveObject);
            }

            return (scene, modularRobotToMultiBodySystemMapping);
        }
    }
}
