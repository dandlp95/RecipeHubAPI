namespace RecipeHubAPI.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        // Execute operations within a database transaction
        Task<TResult> ExecuteInTransaction<TResult>(Func<Task<TResult>> operation);
        
        // For operations that don't return a value
        Task ExecuteInTransaction(Func<Task> operation);
    }
}
