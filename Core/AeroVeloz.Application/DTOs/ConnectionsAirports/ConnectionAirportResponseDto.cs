namespace AeroVeloz.Application.DTOs.ConnectionsAirports
{
    public sealed record ConnectionAirportResponseDto(
        Guid Id,
        string CodeAirlinesIcao,
        string CodeAirportIcao,
        string TokenApi,
        bool IsActive,
        DateTime CreateAt
    );
}