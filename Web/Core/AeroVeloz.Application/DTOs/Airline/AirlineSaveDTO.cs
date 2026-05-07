namespace AeroVeloz.Application.DTOs.Airlines
{
    public sealed record AirlineSaveDto(
        string CodeAirlinesIcao,
        string CodeIata,
        string NameOrganization
    );
}