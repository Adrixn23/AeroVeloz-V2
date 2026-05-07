namespace AeroVeloz.Application.DTOs.Auth
{
    public sealed record UserLoginResultDto(
        Guid UserId,
        string? UserName,
        int OrganizationId,
        string? OrganizationName,
        string? OrganizationType,
        string? RoleName,
        string? Token = null
    );
}
