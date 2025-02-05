using System.ComponentModel.DataAnnotations;

namespace RecipeHubAPI.Models.DTO.User
{
    public class UserUpdate
    {
        [Required]
        public int UserId { get; set; }
        [StringLength(30, ErrorMessage = "Usernames cannot exceed 30 characters.")]
        public required string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
