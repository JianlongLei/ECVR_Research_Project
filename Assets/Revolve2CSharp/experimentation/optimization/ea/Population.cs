using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Revolve2.Database
{
    /// <summary>
    /// Represents a population in the evolutionary process.
    /// </summary>
    /// <typeparam name="TIndividual">The type of the individual associated with the population.</typeparam>
    public class Population<TIndividual>
        where TIndividual : class
    {
        [Key]
        public int Id { get; set; }

        public ICollection<TIndividual> Individuals { get; set; } = new List<TIndividual>();
    }
}
