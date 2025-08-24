using RecipeHubAPI.Models.DTO.RecipeDTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models.DTO.Recipe
{
    public class RecipeStepsDTO
    {
        public int RecipeId { get; set; }
        [Required]
        public required string Name { get; set; }
        public required string CookingTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
        public List<StepDTO> steps { get; set; }
    }
}
