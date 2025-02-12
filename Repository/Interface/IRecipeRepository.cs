using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Models.DTO.Recipe;
using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IRecipeRepository
    {
        Task<RecipeDTO> CreateRecipe(RecipeCreateDTO recipeDTO);
        List<RecipeDTO> GetRecipes(int userId, int? groupId = null, PaginationParams? paginationParams = null);
        RecipeDTO GetRecipe(int id, int userId);
        Task<RecipeDTO> UpdateRecipe(RecipeUpdate recipeDTO, int userId, int recipeId, bool updateAllFields = false);
        Task DeleteRecipe(int id, int userId);
    }
}
