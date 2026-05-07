using AeroVeloz.Application.DTOs.Organization.Base;

namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record AirportUpdateDto(
        int idOrg,
        string? nameOrganization,
        string? emailOrganization,
        string? codeIATA,
        string? codeICAO,
        string? city,
        string? country,
        DateTimeOffset timeOffset,
        bool isActived
        ) : OrganizationBaseDto(nameOrganization,  emailOrganization);
}
