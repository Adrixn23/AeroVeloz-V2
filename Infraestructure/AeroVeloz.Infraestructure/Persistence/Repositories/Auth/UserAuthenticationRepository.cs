using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Common.codeError.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Models.UserSystem;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Auth
{
    public class UserAuthenticationRepository : IUserRepositoryAuthenticacion
    {
        private readonly AeroVelozContext _context;

        public UserAuthenticationRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> BelongsToOrganizationAsync(Guid userId, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
            if (user == null)
                return new ValidationResult().Failur(AuthenticationErrors.UserNotFound);
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> IsOrganizationAccessAllowedAsync(int orgId)
        {
            var org = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == orgId);
            if (org == null || !org.isActived)
                return new ValidationResult().Failur(AuthenticationErrors.NoExistOrgByUsers);
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> IsUserActiveAsync(Guid userId, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
            if (user == null || !user.isActive)
                return new ValidationResult().Failur(AuthenticationErrors.UserInactive);
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> IsUserLockedAsync(Guid userId, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
            if (user == null)
                return new ValidationResult().Failur(AuthenticationErrors.UserNotFound);
            if (user.lockedUntil != null)
                return new ValidationResult().Failur(AuthenticationErrors.UserLocked);
            return new ValidationResult().Success();
        }

        public async Task<bool> RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, byte[] ipAddress, int orgId)
        {
            var rowsAffected = await _context.Users
                .Where(u => u.Id == userId && u.idOrganization == orgId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.failedLoginAttempts, failedLoginAttempts)
                    .SetProperty(u => u.lockedUntil, lockedUntil)
                    .SetProperty(u => u.ipAdress, ipAddress)
                );
            return rowsAffected > 0;
        }

        public async Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.nameUser == username && u.idOrganization == orgId);
            if (user == null)
                return new ValidationResult().Failur(AuthenticationErrors.UserNotFound);

            // Verificación del hash con la contraseña que viene del request.
            var hasher = new PasswordHasher<Domain.Entities.Users.User.User>();
            var result = hasher.VerifyHashedPassword(null!, user.passwordHash!, password);
            
            if (result == PasswordVerificationResult.Success)
                return new ValidationResult().Success();

            return new ValidationResult().Failur(AuthenticationErrors.InvalidCredentials);
        }

        public async Task<UserSystemModel> GetByUserNameAsync(string nameUser, int orgId)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.nameUser == nameUser && u.idOrganization == orgId)
                .Select(u => new UserSystemModel(u.Id, u.nameUser, u.isActive))
                .FirstAsync();
        }
    }
}
