namespace AeroVeloz.Application.DTOs.ConnectionsAirports
{
    public sealed record ConnectionAirportRequestDto(
        string CodeAirlinesIcao,
        string CodeAirportIcao
    );
}