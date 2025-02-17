namespace RecipeHubAPI.Models.DTO.UserDTOs
{
    public class UserLogin
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}
