using System;
using Revolve2.Simulation;
using Revolve2.Geo;
using System.Collections.Generic;
using MVec = Revolve2.Vector;
using System.Numerics;

namespace Revolve2.TerrainGeneration
{
    public static class TerrainFactory
    {
        private const int NumEdges = 100; // Default edge resolution for heightmaps

        /// <summary>
        /// Creates a flat plane terrain.
        /// </summary>
        public static Terrain CreateFlatTerrain(MVec.Vector2 size)
        {
            List<Geometry> list = new List<Geometry>
            {
                new GeometryPlane(new Pose(), size)
            };
            return new Terrain(list);
        }

        /// <summary>
        /// Creates a crater-like terrain with rugged features.
        /// </summary>
        public static Terrain CreateCraterTerrain(Vector2 size, float ruggedness, float curviness, float granularityMultiplier = 1.0f)
        {
            var numEdges = new Vector2(NumEdges * size.X * granularityMultiplier, NumEdges * size.Y * granularityMultiplier);
            var rugged = CreateRuggedHeightmap(size, numEdges, 1.5f);
            var bowl = CreateBowlHeightmap(numEdges);

            float maxHeight = ruggedness + curviness;
            if (maxHeight == 0.0f)
            {
                maxHeight = 1.0f;
            }

            var heightmap = new float[(int)numEdges.X, (int)numEdges.Y];
            for (int i = 0; i < numEdges.X; i++)
            {
                for (int j = 0; j < numEdges.Y; j++)
                {
                    heightmap[i, j] = (ruggedness * rugged[i, j] + curviness * bowl[i, j]) / maxHeight;
                }
            }

            return new Terrain(new List<Geometry>
            {
                new GeometryHeightmap(new Pose(), 0.0f, new Vector3(size.X, size.Y, maxHeight), 0.1f + ruggedness, heightmap)
            });
        }

        private static float[,] CreateRuggedHeightmap(Vector2 size, Vector2 numEdges, float density)
        {
            int width = (int)numEdges.X;
            int height = (int)numEdges.Y;
            float[,] heightmap = new float[width, height];

            const int Octave = 10;
            const float C1 = 4.0f;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float noiseX = x / numEdges.X * C1 * size.X * density;
                    float noiseY = y / numEdges.Y * C1 * size.Y * density;
                    heightmap[x, y] = PerlinNoise(noiseX, noiseY, Octave);
                }
            }

            return heightmap;
        }

        private static float[,] CreateBowlHeightmap(Vector2 numEdges)
        {
            int width = (int)numEdges.X;
            int height = (int)numEdges.Y;
            float[,] heightmap = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float dx = (x / numEdges.X * 2.0f - 1.0f);
                    float dy = (y / numEdges.Y * 2.0f - 1.0f);

                    float distanceSquared = dx * dx + dy * dy;
                    if (Math.Sqrt(distanceSquared) <= 1.0)
                    {
                        heightmap[x, y] = dx * dx + dy * dy;
                    }
                    else
                    {
                        heightmap[x, y] = 0.0f;
                    }
                }
            }

            return heightmap;
        }

        private static float PerlinNoise(float x, float y, int octave)
        {
            // Replace with a Perlin noise library or implementation.
            return (float)new Random().NextDouble(); // Placeholder for noise generation
        }
    }
}
