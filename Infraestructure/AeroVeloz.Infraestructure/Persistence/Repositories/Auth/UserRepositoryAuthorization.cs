using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Common.codeError.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Models.Permission;
using AeroVeloz.Domain.Models.Rol;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Auth
{
    public class UserRepositoryAuthorization : IUserRepositoryAuthorization
    {
        private readonly AeroVelozContext _context;

        public UserRepositoryAuthorization(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> AuthorizeOrganizationAccessAsync(Guid userId, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            if (user == null)
                return new ValidationResult().Failur(AuthenticationErrors.UserNotFound);
            if (user.idOrganization != orgId)
                return new ValidationResult().Failur(AuthorizationErrors.OrganizationAccessDenied);
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanModifyFlightAsync(Guid userId, short flightNumber, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                return new ValidationResult().Failur(errors);
            }
            var org = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == orgId);
            if (org == null || !org.isActived)
            {
                errors.Add(AuthorizationErrors.OrganizationNoActive);
                return new ValidationResult().Failur(errors);
            }
            if (org.typeOrganization != "AIRLINE")
            {
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<RolModel> GetUserRolesAsync(Guid userId, int orgId)
        {
            return await (
                from u in _context.Users.AsNoTracking()
                join r in _context.Roles.AsNoTracking()
                  on u.idRol equals r.Id
                where u.Id == userId && u.idOrganization == orgId
                select new RolModel(r.nameRol!)
            ).FirstAsync();
        }

        public async Task<bool> IsAirlineAdminAsync(Guid userId, int orgId)
        {
            return await HasRoleAsync(userId, orgId, "AIRLINEADMIN");
        }

        public async Task<bool> HasRoleAsync(Guid userId, int orgId, string rolName)
        {
            return await (
                from u in _context.Users.AsNoTracking()
                join r in _context.Roles.AsNoTracking()
                   on u.idRol equals r.Id
                where u.Id == userId && u.idOrganization == orgId && r.nameRol == rolName
                select r.Id
            ).AnyAsync();
        }

        public async Task<IReadOnlyCollection<PermissionModel>> GetUserPermissionsAsync(Guid userId, int orgId)
        {
            return await (
                from u in _context.Users
                join r in _context.RolPermissions on u.idRol equals r.idRol
                join p in _context.Permissions on r.idPermission equals p.Id
                where u.Id == userId && u.idOrganization == orgId
                select new PermissionModel((byte)p.Id, p.codePermision)
            ).ToListAsync();


        }

        public async Task<ValidationResult> CanViewAuditLogsAsync(Guid userId, int orgId)
        {
            //validar que el usuario que esta intenando vizualizar la auditoria de su organizacion exista y contenga los elementos de admin 

            var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == userId && us.idOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.idRol);

            if (!rol!.nameRol!.Contains("ADMIN"))
            {
                errors.Add(AuthorizationErrors.AdminAccessRequired);
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }
    }
}
