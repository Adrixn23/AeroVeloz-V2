using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.Permission;

namespace AeroVeloz.Application.Repositories.Auth
{
    public interface IUserRepositoryAuthenticacion
    {
      
        /* 
         * Estos elementos son los metodos que permiten validar el usuario que esta intentando ingresar asi como realiza bloqueo de
         * direcciones ip en caso de que la ip de donde esta viniendo el logueo no coincide a la ultima ip que el usuario uso para 
         * acceder al sistema
         */

        Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId);
        Task<ValidationResult> IsUserActiveAsync(Guid userId, int orgId);
        Task<ValidationResult> IsUserLockedAsync(Guid userId, int orgId);
        Task<ValidationResult> BelongsToOrganizationAsync(Guid userId, int orgId);
        Task<ValidationResult> IsOrganizationAccessAllowedAsync(int orgId);
        Task<bool> RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, int orgId);


    }
}
