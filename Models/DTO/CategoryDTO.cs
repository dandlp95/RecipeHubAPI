namespace RecipeHubAPI.Models.DTO
{
    public class CategoryDTO
    {
        public int? CategoryId { get; set; }
        public int? UserId { get; set; }
        public int? RecipeId { get; set; }
        public string Title { get; set; }
    }
}
