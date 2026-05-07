namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record ConnectionAirlineByAirportResponseDto(
        Guid connectionId,
        string? airportCode,
        string? airlineCode,
        string? airlineName,
        bool isActive,
        DateTime createAt
    );
}
