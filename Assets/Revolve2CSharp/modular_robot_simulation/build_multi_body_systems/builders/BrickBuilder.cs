using System;
using System.Collections.Generic;
using System.Numerics;
using Revolve2.Simulation;
using Revolve2.Mapping;
using Revolve2.Utilities;
using Revolve2.Geo;
using Revolve2.Converters;
using Revolve2.Robot;

namespace Revolve2.Builders
{
    /// <summary>
    /// A builder for bricks.
    /// </summary>
    public class BrickBuilder : Builder
    {
        private readonly Brick _module;
        private readonly RigidBody _rigidBody;
        private readonly Pose _slotPose;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrickBuilder"/> class.
        /// </summary>
        /// <param name="module">The brick module to build.</param>
        /// <param name="rigidBody">The rigid body to build on.</param>
        /// <param name="slotPose">The pose of the slot.</param>
        public BrickBuilder(Brick module, RigidBody rigidBody, Pose slotPose)
        {
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _rigidBody = rigidBody ?? throw new ArgumentNullException(nameof(rigidBody));
            _slotPose = slotPose ?? throw new ArgumentNullException(nameof(slotPose));
        }

        /// <summary>
        /// Builds the brick onto the robot.
        /// </summary>
        /// <param name="multiBodySystem">The multi-body system of the robot.</param>
        /// <param name="bodyToMultiBodySystemMapping">Mapping from body to multi-body system.</param>
        /// <returns>A list of unbuilt child modules and sensors.</returns>
        public override List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping)
        {
            // Calculate the pose of the brick's center.
            var brickCenterPose = new Pose(
                _slotPose.Position +
                Vector3.Transform(new Vector3(_module.BoundingBox.X / 2.0f, 0.0f, 0.0f), _slotPose.Orientation),
                _slotPose.Orientation
            );

            // Add the brick's geometry to the rigid body.
            _rigidBody.Geometries.Add(new GeometryBox(
                pose: brickCenterPose,
                mass: _module.Mass,
                texture: new Texture(baseColor: ConvertColor.Convert(_module.Color)),
                aabb: new AABB(_module.BoundingBox)
            ));

            // Prepare tasks for unbuilt children.
            var tasks = new List<UnbuiltChild>();

            // Add sensors attached to the brick.
            foreach (var sensor in _module.Sensors.GetAllSensors())
            {
                tasks.Add(new UnbuiltChild(sensor, _rigidBody));
            }

            // Add children modules attached to this brick.
            foreach (var attachmentPoint in _module.AttachmentPoints)
            {
                if (_module.Children.TryGetValue(attachmentPoint.Key, out var child))
                {
                    var unbuiltChild = new UnbuiltChild(child, _rigidBody);
                    unbuiltChild.UpdatePose(
                        brickCenterPose.Position +
                        Vector3.Transform(
                            Vector3.Transform(attachmentPoint.Value.Offset, attachmentPoint.Value.Orientation),
                            brickCenterPose.Orientation
                        ),
                        brickCenterPose.Orientation * attachmentPoint.Value.Orientation
                    );
                    tasks.Add(unbuiltChild);
                }
            }

            return tasks;
        }
    }
}
