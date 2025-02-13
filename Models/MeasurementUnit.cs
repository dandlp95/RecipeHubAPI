using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class MeasurementUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasurementUnitId {  get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Abbreviation { get; set; }
    }
}
