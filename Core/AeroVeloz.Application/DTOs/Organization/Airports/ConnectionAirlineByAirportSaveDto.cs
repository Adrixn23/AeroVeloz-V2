namespace AeroVeloz.Application.DTOs.Organization.Airports
{
    public sealed record ConnectionAirlineByAirportSaveDto(
        string? codeAirlinesIcao,
        string? codeAirportIcao
        );
}
