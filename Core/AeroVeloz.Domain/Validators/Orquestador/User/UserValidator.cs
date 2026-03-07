using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator;


namespace AeroVeloz.Domain.Validators.Orquestador.SuperAdmin
{
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
            //si el usuario llega en blanco se returna el mismo y no se recibe ni se agrega
            if (user == null)
            {
                errors.Add(UserErrors.UserInvalid);
                return new ValidationResult().Failur(errors);
            }
            //verifico que la organziacion a la que se esta intentando vincular el usuario ya exista  y se encuentra activa
            var org = await _domainServiceOrganization.ExistByOrgAsync(user.idOrganization);
            if(org == null)
            {
                errors.Add(UserErrors.OrganizationNotFound);
                return new ValidationResult().Failur(errors);
            }
            if (!org.isActived)
                errors.Add(UserErrors.UserAssociateWithOrganization);
         
            //validar si el usuario ya se encuentra registrado en la organizacion y si esta activo
            var exitsUser = await _domainServiceUser.ExistActiveUserAsync(user.Id);
            var existUserInOrg = await _domainServiceUser.UserNameExistOrganization(user.nameUser, org.Id);

            if (!exitsUser)
                errors.Add(UserErrors.UserIsExist);
            if (!existUserInOrg)
                errors.Add(UserErrors.UserExistInOrganization);
       
            var result = new ValidationResult(); // object que contiene la lista de errores 
            return errors.Any() ? result.Failur(errors) : result.Success();
                
        }
    }


}