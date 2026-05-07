namespace AeroVeloz.Domain.Models.Audit
{

    public sealed record AuditDetailModel(
        Guid AuditId,
        string? AuditTypeName,
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        string? NameOrganization,
        string? NameEntity,
        DateTime OccurredAt,
        string? DataNew
    );
}