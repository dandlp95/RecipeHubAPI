using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class GroupRecipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupRecipeId {  get; set; }
        [Required]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public required Group Group { get; set; }
        [Required]
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public required Recipe Recipe { get; set; }
    }
}
