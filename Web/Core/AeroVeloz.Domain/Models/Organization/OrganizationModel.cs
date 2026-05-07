namespace AeroVeloz.Domain.Models.Organization
{
    public sealed record OrganizationModel(
        int Id,
        string? NameOrganization,
        string? TypeOrganization,
        bool IsActive,
        string? EmailOrganization
    );
}
