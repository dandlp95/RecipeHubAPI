using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Need to add field for image.
namespace RecipeHubAPI.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }
        [Required]
        public required string Name { get; set; }
        public required string CookingTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ICollection<GroupRecipe> GroupRecipes { get; set; }

    }
}
