using System;
using System.Collections.Generic;
using Revolve2.Geo;

namespace Revolve2.Simulation
{
    /// <summary>
    /// Represents terrain consisting of only static geometry.
    /// </summary>
    public class Terrain
    {
        /// <summary>
        /// Gets or sets the static geometry that defines the terrain.
        /// </summary>
        public List<Geometry> StaticGeometry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Terrain"/> class.
        /// </summary>
        /// <param name="staticGeometry">The static geometry for the terrain.</param>
        public Terrain(List<Geometry> staticGeometry)
        {
            StaticGeometry = staticGeometry ?? throw new ArgumentNullException(nameof(staticGeometry));
        }
    }
}
