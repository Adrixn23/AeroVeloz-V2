namespace AeroVeloz.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task ExecuteInTransactionAsync(Func<Task> operation);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
    }
}
