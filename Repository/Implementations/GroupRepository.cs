using Microsoft.EntityFrameworkCore;
using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;

namespace RecipeHubAPI.Repository.Implementations
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public ApplicationDbContext _db;
        public GroupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Group> CreateGroup(Group group)
        {
            //new group automatically sets the created on property to Now
            await CreateEntity(group);
            return group;
        }
        public async Task<List<Group>> GetGroups(int userId, PaginationParams? paginationParams = null)
        {
            Expression<Func<Group, bool>> filter = entities => entities.UserId == userId;
            List<Group> groups = await GetAll(filter);
            return groups;
        }
        public async Task<Group?> GetGroup(int id, int userId) 
        {
            Expression<Func<Group, bool>> filter = entities => entities.UserId == userId && entities.GroupId == id;
            Group? group = await GetEntity(filter);
            return group;
        }
        public async Task<Group> UpdateGroup(Group group, bool updateAllFields = false)
        {
            Expression<Func<Group, bool>> expression = entities => entities.GroupId == group.GroupId;
            Group existingGroup = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified GroupId not found.");
            await UpdateEntity(existingGroup, group, updateAllFields);
            return existingGroup;
        }
        public async Task DeleteGroup(int groupId, int userId)
        {
            Expression<Func<Group, bool>> expression = g => g.GroupId == groupId && g.UserId == userId;
            Group group = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified GroupId not found.");
            await DeleteEntities(group);
        }

    }
}