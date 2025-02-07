namespace RecipeHubAPI.Models.DTO.UserDTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public required string EmailAddress { get; set; }
        public required string UserName { get; set; }
        public required DateTime CreatedDate { get; set; }
    }
}
