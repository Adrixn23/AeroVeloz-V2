
namespace AeroVeloz.Desktop.Services.Interfaces.Auth;

public interface ISessionService
{
    Guid UserId { get; set; }
    int OrgId { get; set; }
    string? Token { get; set; }
    string? UserName { get; set; }

    void SetSession(Guid userId, int orgId, string token, string? userName = null);
    void ClearSession();
}