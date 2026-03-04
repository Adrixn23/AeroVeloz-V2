using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.User;

namespace AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator
{
    public interface IUserValidator
    {
        Task<ValidationResult> ValidateForCreateUser(User user);
    }
}

