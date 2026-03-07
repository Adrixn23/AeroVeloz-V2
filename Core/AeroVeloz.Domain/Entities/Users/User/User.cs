using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Domain.Entities.Users.User;

public partial class User : BEntity<Guid>
{
    public string? nameUser { get; init; }
    public string? passwordHash { get; init; }
    public DateTime createAt { get; init ; }
    public bool isActive { get;init ; }
    public byte[]? ipAdress { get; init ;}
    public DateTime? lastLoginAt { get;init ; }
    public int failedLoginAttempts { get;init ; }
    public DateTime? lockedUntil { get;init ; }
    public int idOrganization { get; init; }
    public short idRol { get; init; }
}

