using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Services.Interfaces
{
    public interface IRecipeService
    {
        Task CreateRecipe(CompleteRecipeDTO completeRecipeDTO);
        Task UpdateRecipe(CompleteRecipeDTO completeRecipeDTO);
        Task DeleteRecipe(int recipeId);
        Task<CompleteRecipeDTO> GetRecipeByRecipeId(int recipeId);
        Task<List<CompleteRecipeDTO>> GetRecipesByGroupId(int userId, int? groupId = null);
    }
}
