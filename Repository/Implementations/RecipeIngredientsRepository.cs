using System.Linq.Expressions;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository.Implementations
{
    public class RecipeIngredientsRepository : Repository<RecipeIngredient>, IRecipeIngredientsRepository
    {
        public RecipeIngredientsRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task AddRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            await CreateEntity(recipeIngredient);
        }
        public async Task AddRecipeIngredients(List<RecipeIngredient> recipeIngredients)
        {
            await CreateEntities(recipeIngredients);
        }
        public async Task UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            await UpdateEntity(recipeIngredient, recipeIngredient, true);
        }

        public async Task<List<RecipeIngredient>> GetRecipeIngredientsByRecipeId(int recipeId)
        {
            Expression<Func<RecipeIngredient, bool>> filter = e => e.RecipeId == recipeId;
            List<RecipeIngredient> recipeIngredients = await GetAll(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            return recipeIngredients;
        }

        public async Task DeleteRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            await DeleteEntities(recipeIngredient);
        }

        public async Task DeleteIngredientsByRecipeId(int recipeId)
        {
            Expression<Func<RecipeIngredient, bool>> filter = e => e.RecipeId == recipeId;
            List<RecipeIngredient> ingredientsToDelete = await GetAll(filter);
            
            if (ingredientsToDelete.Any())
            {
                await DeleteEntities(ingredientsToDelete);
            }
        }
    }
}
