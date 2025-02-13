using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Models.DTO.GroupDTOs;
using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IGroupRepository
    {
        Task<GroupDTO> CreateGroup(GroupUpdate groupDTO);
        Task<List<GroupDTO>> GetGroups(int userId, PaginationParams? paginationParams = null);
        Task<GroupDTO> GetGroup(int id, int userId);
        Task<GroupDTO> UpdateGroup(GroupUpdate recipeDTO, int groupId,int userId, bool updateAllFields = false);
        Task DeleteGroup(int id, int userId);
    }
}
