using System.Linq.Expressions;
using AutoMapper;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository.Implementations
{
    public class StepsRepository : Repository<Step>, IStepsRepository
    {
        private readonly IMapper _mapper;
        public StepsRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task AddStep(StepDTO stepDTO)
        {
            Step step = _mapper.Map<Step>(stepDTO);
            await CreateEntity(step);
        }

        public async Task AddSteps(List<StepDTO> stepDTOs)
        {
            List<Step> steps = _mapper.Map<List<Step>>(stepDTOs);
            await CreateEntities(steps);
        }

        public async Task DeleteStep(StepDTO stepDTO)
        {
            Step step = _mapper.Map<Step>(stepDTO);
            await DeleteEntities(step);
        }

        public async Task<List<StepDTO>> GetStepsByRecipeId(int recipeId)
        {
            Expression<Func<Step, bool>> filter = e => e.RecipeId == recipeId;
            List<Step> steps = await GetAll(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            List<StepDTO> stepDTOs = _mapper.Map<List<StepDTO>>(steps);
            return stepDTOs;
        }

        public async Task UpdateStep(StepDTO stepDTO)
        {
            Step step = _mapper.Map<Step>(stepDTO);
            await UpdateEntity(step, stepDTO, true);
        }

        public async Task DeleteStepsByRecipeId(int recipeId)
        {
            Expression<Func<Step, bool>> filter = e => e.RecipeId == recipeId;
            List<Step> stepsToDelete = await GetAll(filter);
            
            if (stepsToDelete.Any())
            {
                await DeleteEntities(stepsToDelete);
            }
        }
    }
}
