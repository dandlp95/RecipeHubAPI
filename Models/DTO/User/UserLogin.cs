namespace RecipeHubAPI.Models.DTO.User
{
    public class UserLogin
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
