namespace RecipeHubAPI.Models.DTO.RecipeDTOs
{
    public class RecipeIngredientDTO
    {
        public int? RecipeIngredientId { get; set; }
        public int? RecipeId { get; set; }
        public int SortOrder { get; set; }
        public int? MeasurementUnitId { get; set; }
        public double? QuantityNumber { get; set; }
        public string IngredientName { get; set; }
    }
}
