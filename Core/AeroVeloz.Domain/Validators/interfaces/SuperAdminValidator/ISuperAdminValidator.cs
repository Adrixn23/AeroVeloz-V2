// Interface

// Interface
using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.interfaces.SuperAdminValidator
{
    public interface ISuperAdminValidator
    {
        ValidationResult ValidateSuperAdminAccess(Guid userId);
        ValidationResult ValidateUserManagementOperation(Guid requestingUserId, Guid targetUserId, string operation);
        ValidationResult ValidateRoleAssignment(Guid userId, short roleId, int organizationId);
        ValidationResult ValidatePasswordReset(Guid requestingUserId, Guid targetUserId, string newPassword);
        ValidationResult ValidateSystemOperation(Guid userId, string operationType);
        ValidationResult ValidateOrganizationManagement(string code, string type, bool createOperation);
        ValidationResult ValidateBackupOperation(Guid userId);
        ValidationResult ValidateAuditAccess(Guid userId, DateTime? from, DateTime? to);
    }
}

