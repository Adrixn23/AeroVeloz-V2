using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Models;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Auth
{
    public class UserAuthenticationRepository : IUserRepositoryAuthenticacion
    {
        private readonly AeroVelozContext _context;

        public UserAuthenticationRepository(AeroVelozContext context) { 
            _context = context; 
        }
        public async Task<ValidationResult> BelongsToOrganizationAsync(Guid userId, int orgId)
        {
            var errors = new List<ErrosValidationResults>();

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId );
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> IsOrganizationAccessAllowedAsync(int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            var org = await _context.Organizations.FirstOrDefaultAsync(org => org.Id == orgId);
            if(org == null || !org.isActived )
            {
                errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();    
        }

        public async Task<ValidationResult> IsUserActiveAsync(Guid userId, int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id  == userId && u.idOrganization == orgId);
            if(user == null || !user.isActive)
            {
                errors.Add(AuthenticationErrors.UserInactive);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
      
        }

        public async Task<ValidationResult> IsUserLockedAsync(Guid userId, int orgId  )
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
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

        public async Task<bool> RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, int orgId)
        {   
            var rowsAffected = await _context.Users
                .Where(u => u.Id == userId && u.idOrganization == orgId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.failedLoginAttempts, failedLoginAttempts)
                    .SetProperty(u => u.lockedUntil, lockedUntil)
                   
                );

            return rowsAffected > 0;
        }

        public  async Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId)
        {
            var errors = new List<ErrosValidationResults>();
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
    }
}
