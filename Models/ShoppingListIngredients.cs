using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace RecipeHubAPI.Models
{
    public class ShoppingListIngredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingListIngredientsId {  get; set; }
        [Required]
        public int ShoppingListId { get; set; }
        [ForeignKey("ShoppingListId")]
        public required ShoppingList ShoppingList { get; set; }
        public required string Ingredient {  get; set; }
        [Required]
        public int MeasurementUnitId { get; set; }
        [ForeignKey("MeasurementUnitId")]
        public required MeasurementUnit MeasurementUnit { get; set; }
        [Required]
        public int QuantityNumber { get; set; }
    }
}
