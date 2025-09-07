using AutoMapper;
using RecipeHubAPI.Database;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository.Implementations
{
    public class GroupRecipeRepository : Repository<GroupRecipe>, IGroupRecipeRepository
    {
        public IMapper _mapper;
        public ApplicationDbContext _db;
        public GroupRecipeRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
            _db = db;
        }

        public Task DeleteGroupRecipesByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroupRecipesByRecipeId(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupRecipe>> GetGroupRecipeByGroupIdAsync(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupRecipe>> GetGroupRecipeByRecipeIdAsync(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<GroupRecipe> InsertGroupRecipeAsync(GroupRecipe groupRecipe)
        {
            throw new NotImplementedException();
        }

        public Task<GroupRecipe> UpdateGroupRecipeAsync(GroupRecipe groupRecipe)
        {
            throw new NotImplementedException();
        }
    }
}
