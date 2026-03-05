using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.Permission;

namespace AeroVeloz.Application.Repositories.Users.security
{
    public interface IUserRepositoryAuthenticacion
    {
      
        /* 
         * Estos elementos son los metodos que permiten validar el usuario que esta intentando ingresar asi como realiza bloqueo de
         * direcciones ip en caso de que la ip de donde esta viniendo el logueo no coincide a la ultima ip que el usuario uso para 
         * acceder al sistema
         */

        Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId);
        Task<bool> IsUserActiveAsync(string nameUser, int orgId);
        Task<bool> IsUserLockedAsync(string nameUser, int orgId);
        Task<bool> BelongsToOrganizationAsync(string nameUser, int orgId);
        Task<IReadOnlyCollection<Permission>> GetUserPermissionsAsync(string nameUser, int orgId); 
        Task<bool> IsOrganizationAccessAllowedAsync(int orgId);
        Task<bool> RegisterLoginAttemptAsync(string nameUser, int failedLoginAttempts, DateTime lockedUntil, byte[] ipAddress, int orgId);
        

    }
}
