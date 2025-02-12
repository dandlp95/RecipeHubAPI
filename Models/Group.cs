using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }
        [Required]
        public required string Name { get; set; }
        public DateTime CreatedDateOn { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<GroupRecipe> GroupRecipes { get; set; }
    }
}
