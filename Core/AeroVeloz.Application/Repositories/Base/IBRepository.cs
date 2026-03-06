namespace AeroVeloz.Application.Repositories.Base
{
    public interface  IBRepository<TEntity, TId> 
    {
         Task<bool> CreateEntity(TEntity entity);
         Task<bool> UpdateEntity(TEntity entity);
         Task<bool> DeleteEntity(TEntity entity);
    }

}
