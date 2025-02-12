using System.ComponentModel.DataAnnotations;

namespace RecipeHubAPI.Models.DTO.GroupDTOs
{
    public class GroupDTO
    {
        [Required]
        public required string Name { get; set; }
        public DateTime CreatedDateOn { get; set; }
    }
}
