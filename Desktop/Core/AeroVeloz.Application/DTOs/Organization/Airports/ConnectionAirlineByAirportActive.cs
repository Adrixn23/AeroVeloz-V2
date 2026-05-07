namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record ConnectionAirlineByAirportActive(
        string? codeAirport,
        string? codeAirline,
        string? tokenApi
        );   
}
