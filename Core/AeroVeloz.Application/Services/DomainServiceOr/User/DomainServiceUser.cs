using AeroVeloz.Application.Repositories.UseAdmin;
using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Entities.Users;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Application.Services.Orquestador.UserAdmin
{
    public class DomainServiceUser : IDomainServiceUser
    {

        private readonly IUserRepository _userRepository;


        // se recibe por dependency injection lo que es el repository  de la entidad user

        public DomainServiceUser(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }


        public Task<ValidationResult> ValidateOrganizationAssignment(User user, int orgId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidatePermissionRoleAssignment(User user, Roles role, Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateRoleAssignment(User user, Roles role, int orgId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUserActivation(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUserDeactivation(User user)
        {
            throw new NotImplementedException();
        }
    }
}
