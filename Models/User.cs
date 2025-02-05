using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public required string EmailAddress { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Usernames cannot exceed 30 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required byte [] PasswordSalt { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
