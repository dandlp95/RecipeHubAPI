namespace RecipeHubAPI.Models.DTO.UserDTOs
{
    public class UserLogin
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
