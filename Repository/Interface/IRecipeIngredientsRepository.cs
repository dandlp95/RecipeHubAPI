using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IRecipeIngredientsRepository
    {
        Task AddRecipeIngredient(RecipeIngredient recipeIngredient);
        Task AddRecipeIngredients(List<RecipeIngredient> recipeIngredients);
        Task UpdateRecipeIngredient(RecipeIngredient recipeIngredient);
        Task DeleteRecipeIngredient(RecipeIngredient recipeIngredient);
        Task DeleteIngredientsByRecipeId(int recipeId);
        Task<List<RecipeIngredient>> GetRecipeIngredientsByRecipeId(int recipeId);
    }
}
