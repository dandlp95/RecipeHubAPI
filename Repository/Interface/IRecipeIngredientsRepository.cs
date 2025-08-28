using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IRecipeIngredientsRepository
    {
        Task AddRecipeIngredient(RecipeIngredientDTO recipeIngredientDTO);
        Task AddRecipeIngredients(List<RecipeIngredientDTO> recipeIngredientDTOs);
        Task UpdateRecipeIngredient(RecipeIngredientDTO recipeIngredientDTO);
        Task DeleteRecipeIngredient(RecipeIngredientDTO recipeIngredientDTO);
        Task DeleteIngredientsByRecipeId(int recipeId);
        Task<List<RecipeIngredientDTO>> GetRecipeIngredientsByRecipeId(int recipeId);
    }
}
