using AeroVeloz.Domain.Entities.Users;
using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator
{
    public interface IUserValidator
    {
        ValidationResult ValidateRegisterUserFields(User user);
   
    }
}

