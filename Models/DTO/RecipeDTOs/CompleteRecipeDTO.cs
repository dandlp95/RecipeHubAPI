namespace RecipeHubAPI.Models.DTO.RecipeDTOs
{
    public class CompleteRecipeDTO
    {
        public int? RecipeId { get; set; }
        public string Name { get; set; }
        // TODO: Change to CookingTimeValue and CookingTimeUnit
        public string CookingTime { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public List<StepDTO> Steps { get; set; }
        public List<RecipeIngredientDTO> RecipeIngredients { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
