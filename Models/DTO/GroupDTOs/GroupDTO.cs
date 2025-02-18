using System.ComponentModel.DataAnnotations;

namespace RecipeHubAPI.Models.DTO.GroupDTOs
{
    public class GroupDTO
    {
        public required int GroupId { get; set; }
        [Required]
        public required string Name { get; set; }
        public int TotalRecipes { get; set; } = 0;
        public DateTime CreatedDateOn { get; set; }
    }
}
