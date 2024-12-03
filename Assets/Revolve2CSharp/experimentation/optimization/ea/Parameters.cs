using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Revolve2.Database
{
    /// <summary>
    /// Represents a collection of parameters stored as serialized data.
    /// </summary>
    public class Parameters
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public float[] ParameterValues
        {
            get => SerializedParameters?.Split(';').Select(float.Parse).ToArray();
            set => SerializedParameters = string.Join(";", value);
        }

        [Required]
        public string SerializedParameters { get; set; }
    }
}
