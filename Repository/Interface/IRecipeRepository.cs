using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<List<RecipeDTO>> GetRecipes(int userId, int? groupId = null, PaginationParams? paginationParams = null);
        Task<RecipeDTO> GetRecipe(int id, int userId);
        Task<RecipeDTO> UpdateRecipe(RecipeDTO recipeDTO, int userId, int recipeId, bool updateAllFields = false);
        Task DeleteRecipe(int id, int userId);
        Task AddRecipe(RecipeDTO recipeDTO);
    }
}
