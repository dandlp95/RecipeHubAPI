using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models.DTO.Recipe
{
    public class RecipeCreateDTO
    {
        [Required]
        public required string Name { get; set; }
        public required string CookingTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
    }
}
