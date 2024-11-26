using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ModularRobot
{
    /// <summary>
    /// Represents the body of a modular robot.
    /// </summary>
    public class Body
    {
        private Core _core;

        /// <summary>
        /// Gets the core of the body.
        /// </summary>
        public Core Core => _core;

        /// <summary>
        /// Initializes a new instance of the <see cref="Body"/> class.
        /// </summary>
        /// <param name="core">The core of the body.</param>
        public Body(Core core)
        {
            _core = core;
        }

        /// <summary>
        /// Calculates the position of a module in a 3D grid with the core as the center.
        /// </summary>
        /// <param name="module">The module to calculate the position for.</param>
        /// <returns>The calculated position as a <see cref="Vector3"/>.</returns>
        public static Vector3 GridPosition(Module module)
        {
            var position = Vector3.Zero;

            var parent = module.Parent;
            var childIndex = module.ParentChildIndex;

            while (parent != null && childIndex.HasValue)
            {
                if (!parent.Children.TryGetValue(childIndex.Value, out var child) || child == null)
                    throw new KeyNotFoundException("Attachment point not found.");

                if (MathF.Abs(child.Orientation.W % (MathF.PI / 2.0f)) > 1e-5)
                    throw new InvalidOperationException("Invalid module orientation.");

                position = Vector3.Transform(position, child.Orientation.ToMatrix());
                position += Vector3.UnitX;

                if (!parent.AttachmentPoints.TryGetValue(childIndex.Value, out var attachmentPoint))
                    throw new KeyNotFoundException("Attachment point not found.");

                position = Vector3.Transform(position, attachmentPoint.Orientation.ToMatrix());
                position = new Vector3(
                    MathF.Round(position.X),
                    MathF.Round(position.Y),
                    MathF.Round(position.Z)
                );

                childIndex = parent.ParentChildIndex;
                parent = parent.Parent;
            }

            return position;
        }

        /// <summary>
        /// Finds all modules of a specific type within the robot body.
        /// </summary>
        /// <typeparam name="TModule">The type of module to search for.</typeparam>
        /// <param name="exclude">A list of module types to exclude from the search.</param>
        /// <returns>A list of modules of the specified type.</returns>
        public List<TModule> FindModulesOfType<TModule>(List<Type>? exclude = null) where TModule : Module
        {
            return FindModulesRecursive(_core, typeof(TModule), exclude ?? new List<Type>()).OfType<TModule>().ToList();
        }

        private static IEnumerable<Module> FindModulesRecursive(Module module, Type moduleType, List<Type> exclude)
        {
            var modules = new List<Module>();

            if (moduleType.IsAssignableFrom(module.GetType()) && !exclude.Any(e => e.IsAssignableFrom(module.GetType())))
                modules.Add(module);

            foreach (var child in module.Children.Values)
                modules.AddRange(FindModulesRecursive(child, moduleType, exclude));

            return modules;
        }

        /// <summary>
        /// Converts the tree structure of the robot to a grid.
        /// </summary>
        /// <returns>A tuple containing the grid and the position vector of the core.</returns>
        public (Module[,,] Grid, Vector3 CorePosition) ToGrid()
        {
            var gridMaker = new GridMaker();
            return gridMaker.MakeGrid(this);
        }

        private class GridMaker
        {
            private readonly List<int> _x = new List<int>();
            private readonly List<int> _y = new List<int>();
            private readonly List<int> _z = new List<int>();
            private readonly List<Module> _modules = new List<Module>();

            public (Module[,,], Vector3) MakeGrid(Body body)
            {
                MakeGridRecursive(body.Core, Vector3.Zero, Quaternion.Identity);

                int minX = _x.Min(), maxX = _x.Max();
                int minY = _y.Min(), maxY = _y.Max();
                int minZ = _z.Min(), maxZ = _z.Max();

                var depth = maxX - minX + 1;
                var width = maxY - minY + 1;
                var height = maxZ - minZ + 1;

                var grid = new Module[depth, width, height];
                for (int i = 0; i < _x.Count; i++)
                {
                    int x = _x[i];
                    int y = _y[i];
                    int z = _z[i];
                    Module module = _modules[i];

                    grid[x - minX, y - minY, z - minZ] = module;
                }


                return (grid, new Vector3(-minX, -minY, -minZ));
            }

            private void MakeGridRecursive(Module module, Vector3 position, Quaternion orientation)
            {
                AddModule(position, module);

                foreach (var (childIndex, attachmentPoint) in module.AttachmentPoints)
                {
                    if (!module.Children.TryGetValue(childIndex, out var child) || child == null)
                        continue;

                    if (MathF.Abs(child.Orientation.W % (MathF.PI / 2.0f)) > 1e-5)
                        throw new InvalidOperationException("Invalid module orientation.");

                    var rotation = Quaternion.Multiply(Quaternion.Multiply(orientation, attachmentPoint.Orientation), child.Orientation);
                    MakeGridRecursive(child, Vector3.Add(position, Vector3.Transform(Vector3.UnitX, rotation.ToMatrix())), rotation);
                }
            }

            private void AddModule(Vector3 position, Module module)
            {
                _modules.Add(module);
                _x.Add((int)MathF.Round(position.X));
                _y.Add((int)MathF.Round(position.Y));
                _z.Add((int)MathF.Round(position.Z));
            }
        }
    }

    public static class QuaternionExtensions
    {
        /// <summary>
        /// Converts a quaternion to a 4x4 rotation matrix.
        /// </summary>
        /// <param name="quaternion">The quaternion to convert.</param>
        /// <returns>The corresponding rotation matrix.</returns>
        public static Matrix4x4 ToMatrix(this Quaternion quaternion)
        {
            return Matrix4x4.CreateFromQuaternion(quaternion);
        }
    }

}
