using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Models.Permission;
using AeroVeloz.Domain.Models.Rol;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Auth
{
    public class UserRepositoryAuthorization : IUserRepositoryAuthorization
    {

        /*
           var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == userId && us.IdOrganization == orgId); 
             NOTA SE DEBE MODIFICAR ESTOS METODOS DENTRO DE ESTA INTERFACE
                                                                                                                    
            Y LA AUTHENTICATION QUE SE REPITE PARA TENER UNA NO REPETICION DEL CODIGO;
         */

        private readonly AeroVelozContext _context;

        public UserRepositoryAuthorization(AeroVelozContext context)
        {
            _context = context;
        }

 
        public async Task<ValidationResult> AuthorizeOrganizationAccessAsync(Guid userId, int orgId)
        {
            //se verifica que el usuario existe y luego la referencia de si el id de roganizacion que teien es decir
            //si la organizacion dodne se encuentra es la misma que la orgnanizacion a la que se encuntra logueado

            var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            if (user == null)
            {
                return new ValidationResult().Failur(AuthenticationErrors.UserNotFound);
            }
            if(user.idOrganization != orgId)
            {
                return new ValidationResult().Failur(AuthorizationErrors.OrganizationAccessDenied);
            }

            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanModifyFlightAsync(Guid userId, short flightNumber, int orgId)
        {
            //validar si el usuario existe dentro del organismo que se esta consultando o mas bien con el que este se logueo
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.idOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                return new ValidationResult().Failur(errors);
            }

            //validar el organismo institucional para proceder con la consulta de la institucion de vuelo 
            var org = await _context.Organizations.FirstOrDefaultAsync(or => or.Id  == orgId);
            if (org == null)
            {
                errors.Add(AuthorizationErrors.OrganizationsNoValid);
                return new ValidationResult().Failur(errors);
            }
            if (!org.isActived)
            {
                errors.Add(AuthorizationErrors.OrganizationNoActive);
                return new ValidationResult().Failur(errors);
            }
            //validar el tipo de organization que esta intentando realizar cambios en el vuelo ya que solo pueden
            //hacerlos los clientes airports y airlines

            if (org.typeOrganization  != "AIRPORT" || org.typeOrganization !=  "AIRLINE")
            {
                errors.Add(AuthorizationErrors.OrganizationsNoValid);
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanModifyOrganizations(Guid userId, int orgId)
        {
            //validar que el usuario exista a nivel de sistema pero que tambien a su vez el mismo tenga un rol de admin system
            // ya que solo estos puedes modificar elementos organizacionales por lo contrario no puede realizar dichas modificaciones

            var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == userId && us.idOrganization == orgId);
            var errors = new List<ErrosValidationResults>();

            if (user != null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                return new ValidationResult().Failur(errors);
            }

            bool conR = await HasRoleAsync(userId, orgId, "SYSTEMADMIN");
            if (!conR)
            {
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                errors.Add(AuthorizationErrors.SuperAdminAccessRequired);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanModifyUsers(Guid userId, int orgId)
        {
            //validar que el usuario existe dentro de la organization si existe entonces 
            var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == userId && us.idOrganization == orgId); 
         
            var errors = new List<ErrosValidationResults>();
            if (user == null) 
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            //verificar que el rol del usuario tenga el colador ADMIN dentro de su organizacion para entonces proceder con la colocacion
            //de la modificaciondel usuario 
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.idRol);

            if (rol!.nameRol!.Contains("ADMIN"))
            {
                errors.Add(AuthorizationErrors.AdminAccessRequired);
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanViewAuditLogsAsync(Guid userId, int orgId)
        {
            //validar que el usuario que esta intenando vizualizar la auditoria de su organizacion exista y contenga los elementos de admin 
            
            var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == userId && us.idOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if(user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.idRol);

            if (rol!.nameRol!.Contains("ADMIN"))
            {
                errors.Add(AuthorizationErrors.AdminAccessRequired);
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        //
        public async Task<RolModel> GetUserRolesAsync(Guid userId, int orgId)
        {
            ///obtener los roles que tiene el usuario dentro de la organizacion en la cual se acaba de loguear 
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
            //validar si el usuario tiene el rol de airline admin para dar acceso a su aplicativo correspondiente 
            return await HasRoleAsync(userId, orgId, "AIRLINEADMIN");
        }

        public async Task<bool> IsAirportAdminAsync(Guid userId, int orgId)
        {
            //validar si el usuario tiene el rol de airport admin para dar acceso a su aplicativo correspondiente 
            return await HasRoleAsync(userId, orgId, "AIRPORTADMIN");
        }

        public async Task<bool> IsSuperAdminAsync(Guid userId, int orgId)
        {
            //validar si el usuario tiene el rol de super admin del system para dar acceso a su aplicativo correspondiente 

              return await HasRoleAsync(userId, orgId, "SYSTEMADMIN");
        }

        public async Task<bool> HasRoleAsync(Guid userId, int orgId, string rolName)
        {
           return await(
                from u in _context.Users.AsNoTracking()
                join r in _context.Roles.AsNoTracking()
                   on u.idRol equals r.Id
                where 
                u.idRol == r.Id && 
                u.idOrganization == orgId
                && r.nameRol == rolName
                select r.Id
              ).AnyAsync();

        }

        public async Task<IReadOnlyCollection<PermissionModel>> GetUserPermissionsAsync(Guid userId, int orgId)
        {
            var permissions = await (
                     from u in _context.Users
                     join r in _context.RolPermissions
                      on u.idRol equals r.idRol
                     join p in _context.Permissions
                       on r.idPermission equals p.Id
                     select new PermissionModel(
                           p.Id,
                           p.codePermision
                         )

                ).ToListAsync();
            if (permissions.Any())
                return permissions;

            return Array.Empty<PermissionModel>();
        }

       
    }
}
