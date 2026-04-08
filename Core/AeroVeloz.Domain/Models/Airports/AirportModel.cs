namespace AeroVeloz.Domain.Models.Airports
{
    public sealed record AirportModel(
          int id,
          string? codeAirportIcao,
          string? codeAirportIata,
          string? nameOrganization,
          DateTimeOffset timeOffset,
          string? city,
          string? country,
          string? emailOrganization,
          bool isActived,
          string? apiKeyMaster
        );
    
}
