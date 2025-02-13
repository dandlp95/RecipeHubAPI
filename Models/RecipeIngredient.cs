using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class RecipeIngredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeIngredientId { get; set; }
        [Required]
        public required string IngredientName { get; set; }
        [Required]
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public required Recipe Recipe { get; set; }
        [Required]
        public int MeasurementUnitId { get; set; }
        [ForeignKey("MeasurementUnitId")]
        public required MeasurementUnit MeasurementUnit { get; set; }
        public int QuantityNumber { get; set; }
        public required int SortOrder { get; set; }
    }
}
