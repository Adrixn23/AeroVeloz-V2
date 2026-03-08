using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;

namespace AeroVeloz.Application.Repositories.Users
{
    /// <summary>
    /// Interfaz de repositorio para la gestión de usuarios.
    /// Extiende <see cref="IBRepository{TEntity, TId}"/> con operaciones CRUD básicas
    /// y agrega consultas específicas para obtener usuarios por organización
    /// y buscar usuarios por nombre dentro de una organización.
    /// </summary>
    public interface IUserRepository : IBRepository<User, Guid>
    {
        /// <summary>
        /// Obtiene todos los usuarios que pertenecen a una organización específica
        /// con información detallada (rol, organización, estado).
        /// </summary>
        /// <param name="orgId">Identificador de la organización.</param>
        /// <returns>Colección de usuarios con detalle de la organización.</returns>
        Task<IReadOnlyCollection<UserDetailModel>> GetUserByOrganizationsId(int orgId);

        /// <summary>
        /// Busca un usuario por su nombre dentro de una organización específica.
        /// </summary>
        /// <param name="nameUser">Nombre del usuario a buscar.</param>
        /// <param name="orgId">Identificador de la organización donde buscar.</param>
        /// <returns>Modelo del usuario encontrado; null si no existe.</returns>
        Task<UserSystemModel> GetByUserName(string nameUser, int orgId);

    }
}
