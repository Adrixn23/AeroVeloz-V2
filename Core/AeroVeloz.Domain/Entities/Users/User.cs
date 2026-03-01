using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users;

public partial class User : BEntity<Guid>
{
    public string? nameUser { get; init; }
    public string? passwordHash { get; init; }
    public bool isUserMaster { get;init ; }
    public DateTime createAt { get;init ; }
    public bool isActive { get;init ; }
    public byte[]? ipAdress { get; init ;}
    public DateTime? lastLoginAt { get;init ; }
    public int failedLoginAttempts { get;init ; }
    public DateTime? lockedUntil { get;init ; }

}
