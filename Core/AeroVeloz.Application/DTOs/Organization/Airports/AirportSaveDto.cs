using AeroVeloz.Application.DTOs.Organization.Base;
using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record AirportSaveDto(
        string? nameOrganization,
        TypeOrganization Type,
        string? emailOrganization,
        string? codeIATA,
        string? codeICAO,
        string? city,
        string? country,
        TimeZoneInfo timeZone
        ) : OrganizationBaseDto(nameOrganization, Type, emailOrganization);
   
}
