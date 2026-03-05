namespace AeroVeloz.Application.Repositories.Base
{
    public interface IRepository<TEntity> 
    {
         Task<bool> CreateEntity(TEntity entity);
         Task<bool> UpdateEntity(TEntity entity);
         Task<bool> DeleteEntity(TEntity entity);
    }

}
