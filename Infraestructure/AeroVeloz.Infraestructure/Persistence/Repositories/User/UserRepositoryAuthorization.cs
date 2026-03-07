using AeroVeloz.Application.Repositories.Users.security;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
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
            var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == userId);
            if (user == null)
            {
                return new ValidationResult().Failur(AuthenticationErrors.UserNotFound);
            }
            if(user.IdOrganization != orgId)
            {
                return new ValidationResult().Failur(AuthorizationErrors.OrganizationAccessDenied);
            }

            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanModifyFlightAsync(Guid userId, short flightNumber, int orgId)
        {
            //validar si el usuario existe dentro del organismo que se esta consultando o mas bien con el que este se logueo
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId && u.IdOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if (user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                return new ValidationResult().Failur(errors);
            }

            //validar el organismo institucional para proceder con la consulta de la institucion de vuelo 
            var org = await _context.Organizations.FirstOrDefaultAsync(or => or.IdOrganizations == orgId);
            if (org == null)
            {
                errors.Add(AuthorizationErrors.OrganizationsNoValid);
                return new ValidationResult().Failur(errors);
            }
            if (org.IsActive == false)
            {
                errors.Add(AuthorizationErrors.OrganizationNoActive);
                return new ValidationResult().Failur(errors);
            }
            //validar el tipo de organization que esta intentando realizar cambios en el vuelo ya que solo pueden
            //hacerlos los clientes airports y airlines

            var type = Enum.Parse<TypeOrganization>(org.TypeOrganization);

            if (type != TypeOrganization.Airport || type != TypeOrganization.Airport)
            {
                errors.Add(AuthorizationErrors.OrganizationsNoValid);
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            var airline = await _context.Airlines.FirstOrDefaultAsync(ar => ar.IdOrganization == org.IdOrganizations);
            if (airline == null) {

                // colocar aqui metodo que permita indicar que la aerolinea no existe dentro del sistema
                //y returna el validation result correspondiente 
            }
            var flightAirline = await _context.Flights.FirstOrDefaultAsync(fl => fl.FlightNumber == flightNumber && fl.CodeAirlines == airline!.CodeAirlines);

            if (flightAirline == null)
            {
                //colocar aqui los validation result correspondientes mostrando los mensajes de flight no encontrado
                //y aplicando su return correspondiente
            }
            int codeFlightState = (int)FlightStateEnum.EnVuelo;

            if(flightAirline!.FlightStatesId == codeFlightState && user.IdRolNavigation.NameRol != "AIRLINE_ADMIN")
            {
                //colocar aqui los validation result correspondientes mostrando los mensajes de flight en vuelo y que solo airline puede
                //cambiar estados en este punto
                //y aplicando su return correspondiente
            }
            
            //aqui colocas tu otra logia no se si tienes interfaces creadas para validar estos puntos si los tienes la inyectas por dependencia a este repo y validas estas cosas
            //tambien si el usuario esta intentando modificar pero el estado dice que el vuelo esta arrived por ende los usuarios del aiport de destination no pueden hacer cambios
            //o lo contrario 

            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> CanModifyOrganizations(Guid userId, int orgId)
        {
            //validar que el usuario exista a nivel de sistema pero que tambien a su vez el mismo tenga un rol de admin system
            // ya que solo estos puedes modificar elementos organizacionales por lo contrario no puede realizar dichas modificaciones

            var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == userId && us.IdOrganization == orgId);
            var errors = new List<ErrosValidationResults>();

            if (user != null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthenticationErrors.NoExistOrgByUsers);
                return new ValidationResult().Failur(errors);
            }

            if(user!.IdRolNavigation.NameRol  != "SYSTEM_ADMIN")
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
            var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == userId && us.IdOrganization == orgId); 
            // NOTA SE DEBE MODIFICAR ESTOS METODOS DENTRO DE ESTA INTERFACE
                                                                                                                    
            // Y LA AUTHENTICATION QUE SE REPITE PARA TENER UNA NO REPETICION DEL CODIGO;
            var errors = new List<ErrosValidationResults>();
            if (user == null) 
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            //verificar que el rol del usuario tenga el colador ADMIN dentro de su organizacion para entonces proceder con la colocacion
            //de la modificaciondel usuario 
            if (!user.IdRolNavigation.NameRol.Contains("ADMIN"))
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
            
            var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == userId && us.IdOrganization == orgId);
            var errors = new List<ErrosValidationResults>();
            if(user == null)
            {
                errors.Add(AuthenticationErrors.UserNotFound);
                errors.Add(AuthorizationErrors.OrganizationAccessDenied);
                return new ValidationResult().Failur(errors);
            }
            if (!user.IdRolNavigation.NameRol.Contains("ADMIN"))
            {
                errors.Add(AuthorizationErrors.AdminAccessRequired);
                errors.Add(AuthorizationErrors.InsufficientPermissions);
                return new ValidationResult().Failur(errors);
            }
            return new ValidationResult().Success();
        }

        public async Task<IReadOnlyCollection<Roles>> GetUserRolesAsync(Guid userId, int orgId)
        {
            ///obtener los roles que tiene el usuario dentro de la organizacion en la cual se acaba de loguear 
            var roles = await _context.Users
             .Where(us => us.IdUser ==
             userId && us.IdOrganization == orgId)
             .Select(u => new Roles
             {
                 Id = u.IdRolNavigation.IdRol,
                 nameRol = u.IdRolNavigation.NameRol
             }).ToListAsync();
            return roles;
        }
        public async Task<bool> IsAirlineAdminAsync(Guid userId, int orgId)
        {
            //validar si el usuario tiene el rol de airline admin para dar acceso a su aplicativo correspondiente 
            var roles = await _context.Users
                .Where(us => us.IdUser ==
                userId && us.IdOrganization == orgId)
                .Select(u => new Roles
                {
                    Id = u.IdRolNavigation.IdRol,
                    nameRol = u.IdRolNavigation.NameRol
                }).ToListAsync();

            var rolExitsUser =  roles.FirstOrDefault(u => u.nameRol == "AIRLINE_ADMIN");
            return rolExitsUser != null;
        }

        public async Task<bool> IsAirportAdminAsync(Guid userId, int orgId)
        {
            //validar si el usuario tiene el rol de airport admin para dar acceso a su aplicativo correspondiente 
            var roles = await _context.Users
             .Where(us => us.IdUser ==
             userId && us.IdOrganization == orgId)
             .Select(u => new Roles
             {
                 Id = u.IdRolNavigation.IdRol,
                 nameRol = u.IdRolNavigation.NameRol
             }).ToListAsync();

            var rolExitsUser = roles.FirstOrDefault(u => u.nameRol == "AIRPORT_ADMIN");
            return rolExitsUser != null;
        }

        public async Task<bool> IsSuperAdminAsync(Guid userId, int orgId)
        {
            //validar si el usuario tiene el rol de super admin del system para dar acceso a su aplicativo correspondiente 
            var roles = await _context.Users
             .Where(us => us.IdUser ==
             userId && us.IdOrganization == orgId)
             .Select(u => new Roles
             {
                 Id = u.IdRolNavigation.IdRol,
                 nameRol = u.IdRolNavigation.NameRol
             }).ToListAsync();

            var rolExitsUser = roles.FirstOrDefault(u => u.nameRol == "SYSTEM_ADMIN");
            return rolExitsUser != null;
        }

        public async Task<IReadOnlyCollection<Permission>> GetUserPermissionsAsync(Guid userId, int orgId)
        {
            // returnar la lista de permisos del usuario que se logueo dentro del sistema a partir de la organizacion en la que se encuentre
            var permissions = await _context.Users
                .Where(u => u.IdUser == userId && u.IdOrganization == orgId)
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

    }
}
