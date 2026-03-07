using AeroVeloz.Application.Repositories.Users.security;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AeroVeloz.Domain.Entities.Users.User;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
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

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId && u.IdOrganization == orgId);
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<IReadOnlyCollection<Permission>> GetUserPermissionsAsync(Guid userId, int orgId)
        {
            var permissions = await _context.Users
                .Where(u => u.IdUser == userId && u.IdOrganization == orgId )
                .SelectMany(u => u.IdRolNavigation.RolPermissions)
                .Select(rp => new Domain.Entities.Users.Permission.Permission
                {
                    Id = rp.IdPermissionNavigation.IdPermission,
                    codePermision = rp.IdPermissionNavigation.CodePermission,
                    description = rp.IdPermissionNavigation.Description,
                })
                .ToListAsync();

            return permissions;
         }

        public async Task<ValidationResult> IsOrganizationAccessAllowedAsync(int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            var org = await _context.Organizations.FirstOrDefaultAsync(org => org.IdOrganizations == orgId);
            if(org == null || org.IsActive == false)
            {
                errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();    
        }

        public async Task<ValidationResult> IsUserActiveAsync(Guid userId, int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId && u.IdOrganization == orgId);
            if(user == null || user.IsActive == false)
            {
                errors.Add(AuthenticationErrors.UserInactive);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
      
        }

        public async Task<ValidationResult> IsUserLockedAsync(Guid userId, int orgId  )
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId && u.IdOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if(user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                return new ValidationResult().Failur(errors);
            }
            if(user.LockedUntil != null)
            {
                errors.Add(AuthenticationErrors.UserLocked);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<bool> RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, byte[] ipAddress, int orgId)
        {   
            var rowsAffected = await _context.Users
                .Where(u => u.IdUser == userId && u.IdOrganization == orgId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.FailedLoginAttempts, failedLoginAttempts)
                    .SetProperty(u => u.LockedUntil, lockedUntil)
                    .SetProperty(u => u.IpAdress, ipAddress)
                );

            return rowsAffected > 0;
        }

        public  async Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId)
        {
            var errors = new List<ErrosValidationResults>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NameUser.ToLower().Trim() == username.ToLower().Trim() && u.IdOrganization == orgId);
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                return new ValidationResult().Failur(errors);
            }
            var hasher = new PasswordHasher<AeroVeloz.Domain.Entities.Users.User.User>();
            var resultPasswordValid = hasher.VerifyHashedPassword(null!, user.PasswordHash, password);
            if (resultPasswordValid == PasswordVerificationResult.Success)
            {
                return new ValidationResult().Success();
            }
            errors.Add(AuthenticationErrors.InvalidCredentials);
            return new ValidationResult().Failur(errors);
         }
    }
}
