using AeroVeloz.Application.DTOs.Organization.Base;

namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record AirportSaveDto(
        string? nameOrganization,
        string? typeOrganization,
        string? emailOrganization,
        string? codeIATA,
        string? codeICAO,
        string? city,
        string? country,
        DateTimeOffset timeOffset
        ) : OrganizationBaseDto(nameOrganization, typeOrganization, emailOrganization);

}
