using RecipeHubAPI.Database;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository.Implementations
{
    public class MeasurementUnitRepository : Repository<MeasurementUnit>, IMeasurementUnitRepository
    {
        public MeasurementUnitRepository(ApplicationDbContext db) : base(db) {}

        public async Task<List<MeasurementUnit>> GetMeasurementUnits()
        {
            return await GetAll();
        }
    }
}
