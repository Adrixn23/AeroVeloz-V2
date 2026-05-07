using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Auth
{
    public class UserAuthenticationRepository : IUserRepositoryAuthenticacion
    {
        private readonly AeroVelozContext _context;
        private readonly ILogger<UserAuthenticationRepository> _logger;

        public UserAuthenticationRepository(AeroVelozContext context, ILogger<UserAuthenticationRepository> logger) { 
            _context = context; 
            _logger = logger;
        }
        public async Task<ValidationResult> BelongsToOrganizationAsync(Guid userId, int orgId)
        {
            var errors = new List<ErrosValidationResults>();

            try
            {
                var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId );
                if (user == null)
                {
                    errors.Add(AuthenticationErrors.UserNotFound);
                    return new ValidationResult().Failur(errors);
                }
                return new ValidationResult().Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validando pertenencia a organización para usuario {UserId} org {OrgId}", userId, orgId);
                errors.Add(ErrosValidationResults.Create("SERVER_ERROR", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde."));
                return new ValidationResult().Failur(errors);
            }
        }

        public async Task<ValidationResult> IsOrganizationAccessAllowedAsync(int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            try
            {
                var org = await _context.Organizations.FirstOrDefaultAsync(org => org.Id == orgId);
                if(org == null || !org.isActived )
                {
                    errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                    return new ValidationResult().Failur(errors);
                }
                return new ValidationResult().Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando organización {OrgId}", orgId);
                errors.Add(ErrosValidationResults.Create("SERVER_ERROR", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde."));
                return new ValidationResult().Failur(errors);
            }
        }

        public async Task<ValidationResult> IsUserActiveAsync(Guid userId, int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id  == userId && u.idOrganization == orgId);
                if(user == null || !user.isActive)
                {
                    errors.Add(AuthenticationErrors.UserInactive);
                    return new ValidationResult().Failur(errors);
                }
                return new ValidationResult().Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando actividad de usuario {UserId} en org {OrgId}", userId, orgId);
                errors.Add(ErrosValidationResults.Create("SERVER_ERROR", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde."));
                return new ValidationResult().Failur(errors);
            }
        }

        public async Task<ValidationResult> IsUserLockedAsync(Guid userId, int orgId  )
        {
            var errors = new List<ErrosValidationResults>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
                if(user == null)
                {
                    errors.Add(AuthenticationErrors.UserNotFound);
                    return new ValidationResult().Failur(errors);
                }
                if(user.lockedUntil != null && user.lockedUntil > DateTime.UtcNow)
                {
                    errors.Add(AuthenticationErrors.UserLocked);
                    return new ValidationResult().Failur(errors);
                }
                return new ValidationResult().Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando si usuario {UserId} está bloqueado", userId);
                errors.Add(ErrosValidationResults.Create("SERVER_ERROR", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde."));
                return new ValidationResult().Failur(errors);
            }
        }

        public async Task<bool> RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, int orgId)
        {   
            try
            {
                var rowsAffected = await _context.Users
                    .Where(u => u.Id == userId && u.idOrganization == orgId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(u => u.failedLoginAttempts, failedLoginAttempts)
                        .SetProperty(u => u.lockedUntil, lockedUntil)

                    );

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registrando intento de inicio de sesión de usuario {UserId}", userId);
                return false;
            }
        }

        public  async Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.nameUser! == username && u.idOrganization == orgId);
                if (user == null)
                {
                    errors.Add(AuthenticationErrors.UserNotFound);
                    return new ValidationResult().Failur(errors);
                }
                var hasher = new PasswordHasher<Domain.Entities.Users.User.User>();
                var resultPasswordValid = hasher.VerifyHashedPassword(null!, user.passwordHash!, password);
                if (resultPasswordValid == PasswordVerificationResult.Success)
                {
                    return new ValidationResult().Success();
                }
                errors.Add(AuthenticationErrors.InvalidCredentials);
                return new ValidationResult().Failur(errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando credenciales del usuario: {Username}", username);
                errors.Add(ErrosValidationResults.Create("SERVER_ERROR", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde."));
                return new ValidationResult().Failur(errors);
            }
        }
    }
}
