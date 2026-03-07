using AeroVeloz.Domain.Entities.Users;
using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator
{
    public interface IUserValidator
    {
        ValidationResult ValidateRegisterUserFields(User user);
   
    }
}

