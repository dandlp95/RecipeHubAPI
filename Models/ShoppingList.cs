using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHubAPI.Models
{
    public class ShoppingList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingListId { get; set; }
        [Required]
        public required string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
