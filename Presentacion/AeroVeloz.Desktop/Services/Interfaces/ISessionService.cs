using System;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface ISessionService
{
    Guid UserId { get; set; }
    int OrgId { get; set; }
    string? Token { get; set; }
    
    void SetSession(Guid userId, int orgId, string token);
    void ClearSession();
}