namespace RecipeHubAPI.Models.DTO.RecipeDTOs
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string? Name { get; set; }
        public string? CookingTime { get; set; }
        public DateTime? CreatedOn { get; set; }
        // UserId property will be set on controller.
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
