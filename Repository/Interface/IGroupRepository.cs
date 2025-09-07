using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IGroupRepository
    {
        Task<Group> CreateGroup(Group group);
        Task<List<Group>> GetGroups(int userId, PaginationParams? paginationParams = null);
        Task<Group?> GetGroup(int id, int userId);
        Task<Group> UpdateGroup(Group group, bool updateAllFields = false);
        Task DeleteGroup(int groupId, int userId);
    }
}
