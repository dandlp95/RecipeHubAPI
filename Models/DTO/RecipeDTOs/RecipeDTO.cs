namespace RecipeHubAPI.Models.DTO.RecipeDTOs
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? CookingTime { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
