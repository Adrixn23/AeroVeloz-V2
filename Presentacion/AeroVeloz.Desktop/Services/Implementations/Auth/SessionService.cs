using AeroVeloz.Desktop.Services.Interfaces.Auth;

namespace AeroVeloz.Desktop.Services.Implementations.Auth;

public class SessionService : ISessionService
{
    public Guid UserId { get; set; }
    public int OrgId { get; set; }
    public string? Token { get; set; }
    public string? UserName { get; set; }

    public void SetSession(Guid userId, int orgId, string token, string? userName = null)
    {
        UserId = userId;
        OrgId = orgId;
        Token = token;
        UserName = userName;
    }

    public void ClearSession()
    {
        UserId = Guid.Empty;
        OrgId = 0;
        Token = null;
        UserName = null;
    }
}