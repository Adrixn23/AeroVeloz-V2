using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Application.Repositories.Base
{
    public interface IRepository<TEntity, TId> 
    {
         Task<TEntity?>  GetByIdAsync(TId id);
         Task<bool> CreateEntity(TEntity entity);
         Task<bool> UpdateEntity(TEntity entity);
         Task<bool> DeleteEntity(TEntity entity);
    }
}
