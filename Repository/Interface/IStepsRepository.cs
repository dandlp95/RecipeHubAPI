using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IStepsRepository
    {
        Task AddStep(Step step);
        Task AddSteps(List<Step> steps);
        Task UpdateStep(Step step);
        Task DeleteStep(Step step);
        Task DeleteStepsByRecipeId(int recipeId);
        Task<List<Step>> GetStepsByRecipeId(int recipeId);
    }
}
