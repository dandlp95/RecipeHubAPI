using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IMeasurementUnitRepository
    {
        Task<List<MeasurementUnit>> GetMeasurementUnits();
    }
}
