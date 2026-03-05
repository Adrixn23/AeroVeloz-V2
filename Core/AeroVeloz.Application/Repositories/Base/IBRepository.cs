using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Application.Repositories.Base
{
    public interface IBRepository<TEntity, TId> 
    {
        /*repositorio base para loe elementos crud, este repositorio se implementa en los
         * repositorios de application
           que contienen la logica para realizar operaciones que estan ligadas a elementos de Ef core  y persistir la data
        */

         Task<TEntity?>  GetByEntityAsync (TId id);
         Task<bool> CreateEntity(TEntity entity);
         Task<bool> UpdateEntity(TEntity entity);
         Task<bool> DeleteEntity(TEntity entity);
    }
}
