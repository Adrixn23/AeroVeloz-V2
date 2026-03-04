using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.User;
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
            
            var errors = new List<DomainError>();

            if (user == null)
            {
                errors.Add(UserErrors.UserInvalid);
                return new ValidationResult().Failur(errors);
            }

            var org = await _domainServiceOrganization.ExistByOrgAsync(user.idOrganization);
            if(org == null)
            {
                errors.Add(UserErrors.OrganizationNotFound);
                return new ValidationResult().Failur(errors);
            }
            if (!org.isActived)
                errors.Add(UserErrors.UserAssociateWithOrganization);
         
            var exitsUser = await _domainServiceUser.ExistActiveUserAsync(user.Id);
            var existUserInOrg = await _domainServiceUser.UserNameExistOrganization(user.Id, org.Id);

            if (!exitsUser)
                errors.Add(UserErrors.UserIsExist);
            if (!existUserInOrg)
                errors.Add(UserErrors.UserExistInOrganization);
       
            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
                
        }
    }


}