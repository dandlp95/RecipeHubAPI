using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public required User user { get; set; }
        public ICollection<RecipeCategory> RecipeCategories { get; set; }
    }
}
