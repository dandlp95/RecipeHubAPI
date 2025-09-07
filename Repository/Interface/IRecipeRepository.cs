using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<List<Recipe>> GetRecipes(int userId, int? groupId = null, PaginationParams? paginationParams = null);
        Task<Recipe> GetRecipe(int id, int userId);
        Task<Recipe> UpdateRecipe(Recipe recipe, bool updateAllFields = false);
        Task DeleteRecipe(int id, int userId);
        Task<Recipe> AddRecipe(Recipe recipe);
    }
}
