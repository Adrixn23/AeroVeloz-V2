namespace AeroVeloz.Domain.Models.Airports
{
    public sealed record AirlineConnectionByAirportModel(
        Guid connectionId,
        string? airportCode,
        string? airlineCode,
        string? airlineName,
        bool isActive,
        DateTime CreateAt
        );
    
}
