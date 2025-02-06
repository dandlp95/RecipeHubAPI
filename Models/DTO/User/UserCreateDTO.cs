using System.ComponentModel.DataAnnotations;

namespace RecipeHubAPI.Models.DTO.User
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Usernames cannot exceed 30 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public required string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
        public required string ConfirmPassword { get; set; }
    }
}
