using RecipeHubAPI.Models.DTO.RecipeDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IStepsRepository
    {
        Task AddStep(StepDTO stepDTO);
        Task AddSteps(List<StepDTO> stepDTOs);
        Task UpdateStep(StepDTO stepDTO);
        Task DeleteStep(StepDTO stepDTO);
        Task DeleteStepsByRecipeId(int recipeId);
        Task<List<StepDTO>> GetStepsByRecipeId(int recipeId);
    }
}
