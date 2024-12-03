using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revolve2.Database
{
    /// <summary>
    /// Represents an individual in a population.
    /// </summary>
    /// <typeparam name="TGenotype">The type of the genotype associated with the individual.</typeparam>
    public class Individual<TGenotype>
        where TGenotype : class
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Population")]
        public int PopulationId { get; set; }

        [Required]
        public int PopulationIndex { get; set; }

        [ForeignKey("Genotype")]
        public int GenotypeId { get; set; }

        public TGenotype Genotype { get; set; }

        [Required]
        public float Fitness { get; set; }
    }
}
