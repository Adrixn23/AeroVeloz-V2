namespace AeroVeloz.Desktop.Models.DTOs.Organization
{
    public record OrganizationDto(
        int Id,
        string? NameOrganization,
        string? TypeOrganization,
        bool IsActived,
        string? EmailOrganization
    );
}
