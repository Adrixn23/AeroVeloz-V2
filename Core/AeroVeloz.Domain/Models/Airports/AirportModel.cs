namespace AeroVeloz.Domain.Models.Airports
{
    public sealed record AirportModel(
          string? codeAirportIata,
          string? codeAirportIcao,
          string? nameAirport,
          TimeZoneInfo TimeZone,
          string? city,
          string? country
        );
    
}
