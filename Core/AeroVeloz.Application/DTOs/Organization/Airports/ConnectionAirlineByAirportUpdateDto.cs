namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record ConnectionAirlineByAirportUpdateDto(
        Guid Id,
        string? codeAirlinesIcao,
        string? codeAirportIcao,
        bool isActive
        );
}
