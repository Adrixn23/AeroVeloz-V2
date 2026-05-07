namespace AeroVeloz.Application.DTOs.Flights.Base
{
    public interface IBRepository<TEntity>
    {
         Task<bool> CreateEntity(TEntity entity);
         Task<bool> UpdateEntity(TEntity entity);
         Task<bool> DeleteEntity(TEntity entity);
    }
}
