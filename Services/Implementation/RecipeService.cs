using AutoMapper;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;

namespace RecipeHubAPI.Services.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeIngredientsRepository _recipeIngredientsRepository;
        private readonly IStepsRepository _stepsRepository;
        private readonly IMapper _mapper;
        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientsRepository recipeIngredientsRepository, IStepsRepository stepsRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _recipeIngredientsRepository = recipeIngredientsRepository;
            _stepsRepository = stepsRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateRecipe(CompleteRecipeDTO completeRecipeDTO)
        {
            RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(completeRecipeDTO);
            List<RecipeIngredientDTO> recipeIngredientDTOs = completeRecipeDTO.Ingredients;
            List<StepDTO> stepDTOs = completeRecipeDTO.Steps;
            
            await _recipeRepository.ExecuteInTransaction(async () =>
            {
                recipeDTO = await _recipeRepository.AddRecipe(recipeDTO);
                
                // Update all DTOs with the new RecipeId.
                recipeIngredientDTOs.ForEach(i => i.RecipeId = recipeDTO.RecipeId);
                stepDTOs.ForEach(s => s.RecipeId = recipeDTO.RecipeId);

                await _recipeIngredientsRepository.AddRecipeIngredients(recipeIngredientDTOs);
                await _stepsRepository.AddSteps(stepDTOs);
            });

            return recipeDTO.RecipeId;
        }

        public async Task UpdateRecipe(CompleteRecipeDTO completeRecipeDTO, int userId)
        {
            RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(completeRecipeDTO);
            List<RecipeIngredientDTO> recipeIngredientDTOs = completeRecipeDTO.Ingredients;
            List<StepDTO> stepDTOs = completeRecipeDTO.Steps;
            
            await _recipeRepository.ExecuteInTransaction(async () =>
            {
                // Update recipe
                await _recipeRepository.UpdateRecipe(recipeDTO, userId, recipeDTO.RecipeId);

                // Delete old ingredients and steps
                await _recipeIngredientsRepository.DeleteIngredientsByRecipeId(recipeDTO.RecipeId);
                await _stepsRepository.DeleteStepsByRecipeId(recipeDTO.RecipeId);

                // Add new ones (much faster than individual updates)
                if (recipeIngredientDTOs.Any())
                {
                    await _recipeIngredientsRepository.AddRecipeIngredients(recipeIngredientDTOs);
                }
                if (stepDTOs.Any())
                {
                    await _stepsRepository.AddSteps(stepDTOs);
                }
            });
        }

        public async Task DeleteRecipe(int recipeId, int userId)
        {
            await _recipeRepository.ExecuteInTransaction(async () =>
            {
                await _recipeRepository.DeleteRecipe(recipeId, userId);
                await _recipeIngredientsRepository.DeleteIngredientsByRecipeId(recipeId);
                await _stepsRepository.DeleteStepsByRecipeId(recipeId);
            });
        }

        public async Task<CompleteRecipeDTO> GetRecipeById(int recipeId, int userId)
        {
            CompleteRecipeDTO completeRecipeDTO = await _recipeRepository.ExecuteInTransaction(async () =>
            {
                RecipeDTO recipeDTO = await _recipeRepository.GetRecipe(recipeId, userId);
                List<RecipeIngredientDTO> recipeIngredientDTOs = await _recipeIngredientsRepository.GetRecipeIngredientsByRecipeId(recipeId);
                List<StepDTO> stepDTOs = await _stepsRepository.GetStepsByRecipeId(recipeId);
                CompleteRecipeDTO completeRecipeDTO = _mapper.Map<CompleteRecipeDTO>(recipeDTO);
                completeRecipeDTO.Ingredients = recipeIngredientDTOs;
                completeRecipeDTO.Steps = stepDTOs;
                return completeRecipeDTO;
            });
            return completeRecipeDTO;
        }

        public async Task<List<CompleteRecipeDTO>> GetRecipesByGroupId(int userId, int? groupId = null)
        {
            List<CompleteRecipeDTO> completeRecipeDTOs = await _recipeRepository.ExecuteInTransaction(async () =>
            {
                List<RecipeDTO> recipeDTOs = await _recipeRepository.GetRecipes(userId, groupId);
                List<CompleteRecipeDTO> completeRecipeDTOs = _mapper.Map<List<CompleteRecipeDTO>>(recipeDTOs);
                foreach (var completeRecipeDTO in completeRecipeDTOs)
                {
                    List<RecipeIngredientDTO> recipeIngredientDTOs = await _recipeIngredientsRepository.GetRecipeIngredientsByRecipeId(completeRecipeDTO.RecipeId);
                    List<StepDTO> stepDTOs = await _stepsRepository.GetStepsByRecipeId(completeRecipeDTO.RecipeId);
                    completeRecipeDTO.Ingredients = recipeIngredientDTOs;
                    completeRecipeDTO.Steps = stepDTOs;
                }

                return completeRecipeDTOs;
            });
            
            return completeRecipeDTOs;
        }

    }
}
