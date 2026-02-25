using AeroVeloz.Domain.Entities.Security;
using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.SuperAdmin;
using System.Text.RegularExpressions;
using AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator;

namespace AeroVeloz.Domain.Validators.Orquestador.SuperAdmin
{
    public class SuperAdminValidator : ISuperAdminValidator
    {
        private readonly Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        private readonly Regex _organizationCodeRegex = new Regex(@"^[A-Z0-9]{3,4}$");

        public ValidationResult ValidateSuperAdminAccess(Guid userId)
        {
            var errors = new List<DomainError>();

            if (userId == Guid.Empty)
                errors.Add(SuperAdminErrors.SuperAdminAccessRequired);


            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateUserManagementOperation(Guid requestingUserId, Guid targetUserId, string operation)
        {
            var errors = new List<DomainError>();

            if (requestingUserId == Guid.Empty || targetUserId == Guid.Empty)
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            if (requestingUserId == targetUserId && (operation == "deactivate" || operation == "delete"))
                errors.Add(SuperAdminErrors.CannotDeactivateSelf);

            if (string.IsNullOrWhiteSpace(operation))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            var validOperations = new[] { "activate", "deactivate", "reset_password", "assign_role", "remove_role" };
            if (!validOperations.Contains(operation.ToLower()))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateRoleAssignment(Guid userId, short roleId, int organizationId)
        {
            var errors = new List<DomainError>();

            if (userId == Guid.Empty)
                errors.Add(SuperAdminErrors.RoleAssignmentFailed);

            if (roleId <= 0 || roleId > 10) 
                errors.Add(SuperAdminErrors.RoleAssignmentFailed);

            if (organizationId <= 0)
                errors.Add(SuperAdminErrors.OrganizationNotFound);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidatePasswordReset(Guid requestingUserId, Guid targetUserId, string newPassword)
        {
            var errors = new List<DomainError>();

            if (requestingUserId == Guid.Empty || targetUserId == Guid.Empty)
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            if (string.IsNullOrWhiteSpace(newPassword))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            if (!_passwordRegex.IsMatch(newPassword))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateSystemOperation(Guid userId, string operationType)
        {
            var errors = new List<DomainError>();

            if (userId == Guid.Empty)
                errors.Add(SuperAdminErrors.SuperAdminAccessRequired);

            if (string.IsNullOrWhiteSpace(operationType))
                errors.Add(SuperAdminErrors.InvalidSystemOperation);

            var validSystemOperations = new[] { "backup", "maintenance", "audit_cleanup", "system_reset", "config_update" };
            if (!validSystemOperations.Contains(operationType.ToLower()))
                errors.Add(SuperAdminErrors.InvalidSystemOperation);

            var maintenanceOperations = new[] { "system_reset", "maintenance", "config_update" };
            if (maintenanceOperations.Contains(operationType.ToLower()))
            {
               
                var currentHour = DateTime.UtcNow.Hour;
                if (currentHour < 2 || currentHour > 4)
                    errors.Add(SuperAdminErrors.MaintenanceWindowRequired);
            }

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateOrganizationManagement(string code, string type, bool createOperation)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrWhiteSpace(code) || !_organizationCodeRegex.IsMatch(code))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            if (string.IsNullOrWhiteSpace(type))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            var validTypes = new[] { "Airport", "Airline", "System" };
            if (!validTypes.Contains(type))
                errors.Add(SuperAdminErrors.InvalidUserManagementOperation);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateBackupOperation(Guid userId)
        {
            var errors = new List<DomainError>();

            if (userId == Guid.Empty)
                errors.Add(SuperAdminErrors.SuperAdminAccessRequired);


            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateAuditAccess(Guid userId, DateTime? from, DateTime? to)
        {
            var errors = new List<DomainError>();

            if (userId == Guid.Empty)
                errors.Add(SuperAdminErrors.SuperAdminAccessRequired);

            if (from.HasValue && to.HasValue && from > to)
                errors.Add(SuperAdminErrors.InvalidSystemOperation);

            if (from.HasValue && from > DateTime.UtcNow)
                errors.Add(SuperAdminErrors.InvalidSystemOperation);

    
            if (from.HasValue && from < DateTime.UtcNow.AddYears(-1))
                errors.Add(SuperAdminErrors.InvalidSystemOperation);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }
    }
}
