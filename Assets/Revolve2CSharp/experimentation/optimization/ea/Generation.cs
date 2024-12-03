using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revolve2.Database
{
    /// <summary>
    /// Represents a generation in the evolutionary process.
    /// </summary>
    /// <typeparam name="TPopulation">The type of the population associated with the generation.</typeparam>
    public class Generation<TPopulation>
        where TPopulation : class
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GenerationIndex { get; set; }

        [ForeignKey("Population")]
        public int PopulationId { get; set; }

        public TPopulation Population { get; set; }
    }
}
