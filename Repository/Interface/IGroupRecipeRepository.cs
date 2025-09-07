using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IGroupRecipeRepository
    {
        Task<List<GroupRecipe>> GetGroupRecipeByRecipeIdAsync(int recipeId);
        Task<   List<GroupRecipe>> GetGroupRecipeByGroupIdAsync(int recipeId);
        Task<GroupRecipe> InsertGroupRecipeAsync(GroupRecipe groupRecipe);
        Task<GroupRecipe> UpdateGroupRecipeAsync(GroupRecipe groupRecipe);
        Task DeleteGroupRecipesByRecipeId(int recipeId);
        Task DeleteGroupRecipesByGroupId(int groupId);
    }
}
