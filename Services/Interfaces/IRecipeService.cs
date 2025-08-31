using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<int> CreateRecipe(CompleteRecipeDTO completeRecipeDTO);
        Task UpdateRecipe(CompleteRecipeDTO completeRecipeDTO, int userId);
        Task DeleteRecipe(int recipeId, int userId);
        Task<CompleteRecipeDTO> GetRecipeById(int recipeId, int userId);
        Task<List<CompleteRecipeDTO>> GetRecipesByGroupId(int userId, int? groupId = null);
        
    }
}
