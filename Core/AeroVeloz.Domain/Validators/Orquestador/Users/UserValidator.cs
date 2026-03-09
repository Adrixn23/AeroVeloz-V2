using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Validators.interfaces.Users;


namespace AeroVeloz.Domain.Validators.Orquestador.Users
{
    /// <summary>
    /// Implementación del validador de usuarios que orquesta las reglas de negocio
    /// para la creación de usuarios. Verifica la existencia y estado de la organización,
    /// la duplicidad del usuario en el sistema y dentro de la organización.
    /// </summary>
    public class UserValidator : IUserValidator
    {

        private readonly IDomainServiceUser _domainServiceUser;
        private readonly IDomainServiceOrganization _domainServiceOrganization;  

        public UserValidator(IDomainServiceUser domainServiceUser, IDomainServiceOrganization domainServiceOrganization)
        {
            _domainServiceOrganization = domainServiceOrganization;
            _domainServiceUser = domainServiceUser;
        }

      
        public async Task<ValidationResult> ValidateForCreateUser(User user)
        {
            var errors = new List<ErrosValidationResults>();

            // Si el usuario llega nulo se retorna inmediatamente con error de invalidez
            if (user == null)
            {
                errors.Add(UserErrors.UserInvalid);
                return new ValidationResult().Failur(errors);
            }

            // Verificar que la organización a la que se vincula el usuario exista y esté activa
            var org = await _domainServiceOrganization.GetByIdAsync(user.idOrganization);
            if(org == null)
            {
                errors.Add(UserErrors.OrganizationNotFound);
                return new ValidationResult().Failur(errors);
            }
            if (!org.IsActive)
                errors.Add(UserErrors.UserAssociateWithOrganization);

            // Validar si el usuario ya se encuentra registrado en el sistema y en la organización
            var exitsUser = await _domainServiceUser.ExistActiveUserAsync(user.Id);
            var existUserInOrg = await _domainServiceUser.UserNameExistOrganization(user.nameUser, org.Id);

            if (!exitsUser)
                errors.Add(UserErrors.UserIsExist);
            if (!existUserInOrg)
                errors.Add(UserErrors.UserExistInOrganization);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();

        }
    }


}