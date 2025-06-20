namespace RecipeHubAPI.Models.DTO.RecipeDTOs
{
    public class StepDTO
    {
        public int RecipeId { get; set; }
        public required string Text { get; set; }
        public required int SortOrder { get; set; }
    }
}
