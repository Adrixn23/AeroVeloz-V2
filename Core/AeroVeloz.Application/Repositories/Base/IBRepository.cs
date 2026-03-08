using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Application.Repositories.Base
{
    /// <summary>
    /// Interfaz genérica base para todos los repositorios del sistema.
    /// Define las operaciones CRUD básicas (crear, actualizar, eliminar) que todos
    /// los repositorios deben implementar. Sigue el patrón Repository de DDD.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad de dominio que gestiona el repositorio.</typeparam>
    /// <typeparam name="TId">Tipo del identificador de la entidad.</typeparam>
    public interface IBRepository<TEntity, TId> 
    {
         /// <summary>
         /// Crea una nueva entidad en la base de datos.
         /// </summary>
         /// <param name="entity">Entidad a persistir.</param>
         /// <returns>True si la entidad fue creada exitosamente.</returns>
         Task<bool> CreateEntity(TEntity entity);

         /// <summary>
         /// Actualiza una entidad existente en la base de datos.
         /// </summary>
         /// <param name="entity">Entidad con los datos actualizados.</param>
         /// <returns>True si la entidad fue actualizada exitosamente.</returns>
         Task<bool> UpdateEntity(TEntity entity);

         /// <summary>
         /// Elimina (desactiva) una entidad de la base de datos mediante borrado lógico.
         /// </summary>
         /// <param name="entity">Entidad a desactivar.</param>
         /// <returns>True si la entidad fue desactivada exitosamente.</returns>
         Task<bool> DeleteEntity(TEntity entity);
    }
}
