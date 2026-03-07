namespace AeroVeloz.Domain.Models.Airports
{
    public sealed record AirportModel(
          string? codeAirportIcao,
          string? codeAirportIata,
          string? nameAirport,
          DateTimeOffset TimeZone,
          string? city,
          string? country
        );
    
}
