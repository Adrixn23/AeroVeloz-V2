using System;
using AeroVeloz.Desktop.Services.Interfaces;

namespace AeroVeloz.Desktop.Services.Implementations;

public class SessionService : ISessionService
{
    public Guid UserId { get; set; }
    public int OrgId { get; set; }
    public string? Token { get; set; }

    public void SetSession(Guid userId, int orgId, string token)
    {
        UserId = userId;
        OrgId = orgId;
        Token = token;
    }

    public void ClearSession()
    {
        UserId = Guid.Empty;
        OrgId = 0;
        Token = null;
    }
}