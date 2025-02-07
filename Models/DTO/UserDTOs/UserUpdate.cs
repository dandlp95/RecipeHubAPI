using System.ComponentModel.DataAnnotations;

namespace RecipeHubAPI.Models.DTO.UserDTOs
{
    public class UserUpdate
    {
        [Required]
        public int UserId { get; set; }
        [StringLength(30, ErrorMessage = "Usernames cannot exceed 30 characters.")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
