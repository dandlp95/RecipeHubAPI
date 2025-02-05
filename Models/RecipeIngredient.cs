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
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public required Ingredient Ingredient { get; set; }
        [Required]
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public required Recipe Recipe { get; set; }
        [Required]
        public int MeasurementUnitId { get; set; }
        [ForeignKey("MeasurementUnitId")]
        public required MeasurementUnit MeasurementUnit { get; set; }
        public int QuantityNumber { get; set; }
    }
}
