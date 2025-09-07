using System.Linq.Expressions;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository.Implementations
{
    public class StepsRepository : Repository<Step>, IStepsRepository
    {
        public StepsRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task AddStep(Step step)
        {
            await CreateEntity(step);
        }

        public async Task AddSteps(List<Step> steps)
        {
            await CreateEntities(steps);
        }

        public async Task DeleteStep(Step step)
        {
            await DeleteEntities(step);
        }

        public async Task<List<Step>> GetStepsByRecipeId(int recipeId)
        {
            Expression<Func<Step, bool>> filter = e => e.RecipeId == recipeId;
            List<Step> steps = await GetAll(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            return steps;
        }

        public async Task UpdateStep(Step step)
        {
            await UpdateEntity(step, step, true);
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
