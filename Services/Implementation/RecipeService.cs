using AutoMapper;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientsRepository recipeIngredientsRepository, IStepsRepository stepsRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _recipeIngredientsRepository = recipeIngredientsRepository;
            _stepsRepository = stepsRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateRecipe(CompleteRecipeDTO completeRecipeDTO)
        {
            RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(completeRecipeDTO);
            List<RecipeIngredientDTO> recipeIngredientDTOs = completeRecipeDTO.RecipeIngredients;
            List<StepDTO> stepDTOs = completeRecipeDTO.Steps;
            List<CategoryDTO> categoryDTOs = completeRecipeDTO.Categories;
            
            await _recipeRepository.ExecuteInTransaction(async () =>
            {
                recipeDTO = await _recipeRepository.AddRecipe(recipeDTO);
                // GroupRecipeDTO groupRecipeDTO = new GroupRecipeDTO(){
                //     GroupId = completeRecipeDTO.GroupId,
                //     RecipeId = recipeDTO.RecipeId
                // };
                
                // await _groupRecipeRepository.AddGroupRecipe(groupRecipeDTO);

                // Update all DTOs with the new RecipeId.
                recipeIngredientDTOs.ForEach(i => i.RecipeId = recipeDTO.RecipeId);
                stepDTOs.ForEach(s => s.RecipeId = recipeDTO.RecipeId);
                categoryDTOs.ForEach(c => c.RecipeId = recipeDTO.RecipeId);

                await _recipeIngredientsRepository.AddRecipeIngredients(recipeIngredientDTOs);
                await _stepsRepository.AddSteps(stepDTOs);
                
                // Add categories
                if (categoryDTOs.Any())
                {
                    List<Category> categories = _mapper.Map<List<Category>>(categoryDTOs);
                    await _categoryRepository.CreateCategories(categories);
                }
            });

            return recipeDTO.RecipeId;
        }

        public async Task UpdateRecipe(CompleteRecipeDTO completeRecipeDTO, int userId)
        {
            RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(completeRecipeDTO);
            List<RecipeIngredientDTO> recipeIngredientDTOs = completeRecipeDTO.RecipeIngredients;
            List<StepDTO> stepDTOs = completeRecipeDTO.Steps;                   
            List<CategoryDTO> categoryDTOs = completeRecipeDTO.Categories;
            
            await _recipeRepository.ExecuteInTransaction(async () =>
            {   
                // Update recipe
                await _recipeRepository.UpdateRecipe(recipeDTO, userId, recipeDTO.RecipeId);

                // Delete old ingredients, steps, and categories
                await _recipeIngredientsRepository.DeleteIngredientsByRecipeId(recipeDTO.RecipeId);
                await _stepsRepository.DeleteStepsByRecipeId(recipeDTO.RecipeId);
                await _categoryRepository.DeleteCategoryByRecipeId(recipeDTO.RecipeId, userId);

                // Add new ones (much faster than individual updates)
                if (recipeIngredientDTOs.Any())
                {
                    await _recipeIngredientsRepository.AddRecipeIngredients(recipeIngredientDTOs);
                }
                if (stepDTOs.Any())
                {
                    await _stepsRepository.AddSteps(stepDTOs);
                }
                if (categoryDTOs.Any())
                {
                    List<Category> categories = _mapper.Map<List<Category>>(categoryDTOs);
                    await _categoryRepository.CreateCategories(categories);
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
                await _categoryRepository.DeleteCategoryByRecipeId(recipeId, userId);
            });
        }

        public async Task<CompleteRecipeDTO> GetRecipeById(int recipeId, int userId)
        {
            CompleteRecipeDTO completeRecipeDTO = await _recipeRepository.ExecuteInTransaction(async () =>
            {
                RecipeDTO recipeDTO = await _recipeRepository.GetRecipe(recipeId, userId);
                List<RecipeIngredientDTO> recipeIngredientDTOs = await _recipeIngredientsRepository.GetRecipeIngredientsByRecipeId(recipeId);
                List<StepDTO> stepDTOs = await _stepsRepository.GetStepsByRecipeId(recipeId);
                List<Category> categories = await _categoryRepository.GetCategoryByRecipeId(recipeId, userId);
                List<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
                CompleteRecipeDTO completeRecipeDTO = _mapper.Map<CompleteRecipeDTO>(recipeDTO);
                completeRecipeDTO.RecipeIngredients = recipeIngredientDTOs;
                completeRecipeDTO.Steps = stepDTOs;
                completeRecipeDTO.Categories = categoryDTOs;
                return completeRecipeDTO;
            });
            return completeRecipeDTO;
        }

        public async Task<List<CompleteRecipeDTO>> GetRecipesByGroupId(int userId, int? groupId = null)
        {
            List<CompleteRecipeDTO> completeRecipeDTOs = await _recipeRepository.ExecuteInTransaction(async () =>
            {
                List<RecipeDTO> recipeDTOs = await _recipeRepository.GetRecipes(userId, groupId);
                if(!recipeDTOs.Any() || recipeDTOs == null )
                {
                    return [];
                }

                List<CompleteRecipeDTO> completeRecipeDTOs = _mapper.Map<List<CompleteRecipeDTO>>(recipeDTOs);
                foreach (var completeRecipeDTO in completeRecipeDTOs)
                {
                    List<RecipeIngredientDTO> recipeIngredientDTOs = await _recipeIngredientsRepository.GetRecipeIngredientsByRecipeId(completeRecipeDTO?.RecipeId ?? 0);
                    List<StepDTO> stepDTOs = await _stepsRepository.GetStepsByRecipeId(completeRecipeDTO?.RecipeId ?? 0);
                    List<Category> categories = await _categoryRepository.GetCategoryByRecipeId(completeRecipeDTO?.RecipeId ?? 0, userId);
                    List<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
                    completeRecipeDTO.RecipeIngredients = recipeIngredientDTOs;
                    completeRecipeDTO.Steps = stepDTOs;
                    completeRecipeDTO.Categories = categoryDTOs;
                }

                return completeRecipeDTOs;
            });
            
            return completeRecipeDTOs;
        }
 
    }
}
