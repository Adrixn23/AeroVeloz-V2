namespace AeroVeloz.Domain.Models.Airports
{
    public sealed record AirlineConnectionByAirportModel(
        string? airportCode,
        string? airlineCode,
        string? airlineName,
        bool isActive,
        DateTime CreateAt
        );
    
}
