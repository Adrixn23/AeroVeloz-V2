namespace AeroVeloz.Application.Repositories.Base
{
    public interface IRepository<TEntity> 
    {

         Task<IEnumerable<TEntity>> GetEntitiesAsync();
         Task<TEntity> GetEntityAsync(int idEntity);
         Task<bool> CreateEntity(TEntity entity);
         Task<bool> UpdateEntity(TEntity entity);
         Task<bool> DeleteEntity(TEntity entity);


    }

}
