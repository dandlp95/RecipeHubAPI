using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;

namespace RecipeHubAPI.Repository.Implementations
{
    public class RecipeRepository:Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Recipe>> GetRecipes(int userId, int? groupId = null, PaginationParams? paginationParams = null)
        {
            // Not sure if I will implement pagination for now...
            List<Recipe> results = [];
            Expression<Func<Recipe, bool>> filter;
            if (groupId == null)
            {
                filter = entity => entity.UserId == userId;
            }
            else
            {
                filter = entity => entity.GroupRecipes.Any(rg => rg.GroupId == groupId);
            }
            results = await GetAll(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            return results;
        }
        public async Task<Recipe> GetRecipe(int id, int userId)
        {
            Expression<Func<Recipe, bool>> filter = e => e.RecipeId == id && e.UserId == userId;
            Recipe recipe = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Recipe not found or access denied.");
            return recipe;
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe, bool updateAllFields = false) 
        {
            Expression<Func<Recipe, bool>> filter = e => e.RecipeId == recipe.RecipeId && e.UserId == recipe.UserId;
            Recipe existingRecipe = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Recipe not found or access denied.");

            await UpdateEntity(existingRecipe, recipe, updateAllFields);
            return existingRecipe;
        } 

        public async Task DeleteRecipe(int id, int userId)
        {
            Expression<Func<Recipe, bool>> filter = e => e.RecipeId == id && e.UserId == userId;
            Recipe recipe = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Recipe not found or access denied");
            await DeleteEntities(recipe);
        }

        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            await CreateEntity(recipe);
            return recipe;
        }
    }
}
