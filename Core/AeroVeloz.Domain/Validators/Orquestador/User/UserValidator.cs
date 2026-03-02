using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Users;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.User;
using AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator;
using System.Text.RegularExpressions;

namespace AeroVeloz.Domain.Validators.Orquestador.SuperAdmin
{
    public class UserValidator : IUserValidator
    {

        private Regex _nameRegex = new Regex(@"[^a-zA-Z]");
        private readonly Regex _passwordRegex = 
            new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        public ValidationResult ValidateRegisterUserFields(User user)
        {

            var errors = new List<DomainError>();
            if (user == null)
            {
                errors.Add(UserErrors.UserInvalid);
                return new ValidationResult().Failur(errors);
            }

            if (user.Id == Guid.Empty)
                errors.Add(UserErrors.InvalidIdUser);

            if (user.nameUser == null || user.nameUser == ""  || !_nameRegex.IsMatch(user.nameUser))
                errors.Add(UserErrors.InvalidNameUser);

            if (user.passwordHash == null || user.passwordHash == "" ||
                user.passwordHash.Length < 8 || !_passwordRegex.IsMatch(user.passwordHash))
                errors.Add(UserErrors.InvalidPasswordUser);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }
    }


}