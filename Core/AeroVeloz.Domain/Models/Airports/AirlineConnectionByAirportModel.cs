namespace AeroVeloz.Domain.Models.Airports
{
    public sealed record AirlineConnectionByAirportModel(
        string? airportCode,
        string? airlineCode,
        bool isActive,
        DateTime CreateAt,
        string? tokenApi
        );
    
}
