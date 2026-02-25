using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users;

public partial class User : BEntity<Guid>
{
    public string nameUser { get; private set; }
    public string passwordHash { get; private set; }
    public bool isUserMaster { get; private set; }
    public DateTime createAt { get; private set; }
    public bool isActive { get; private set; }
    public DateTime? lastLoginAt { get; private set; }
    public int failedLoginAttempts { get; private set; }
    public DateTime? lockedUntil { get; private set; }

    private User(Guid id, string nameUser, string passwordHash, bool isUserMaster)
    {
        this.Id = id;
        this.nameUser = nameUser;
        this.passwordHash = passwordHash;
        this.isUserMaster = isUserMaster;
        this.createAt = DateTime.UtcNow;
        this.isActive = true;
        this.failedLoginAttempts = 0;
    }

    public static User CreateUser(string nameUser, string passwordHash, bool isUserMaster = false)
    {
        return new User(Guid.NewGuid(), nameUser, passwordHash, isUserMaster);
    }

    public void UpdateLastLogin()
    {
        lastLoginAt = DateTime.UtcNow;
        failedLoginAttempts = 0;
        lockedUntil = null;
    }

    public void RegisterFailedLogin()
    {
        failedLoginAttempts++;
        if (failedLoginAttempts >= 3)
        {
            lockedUntil = DateTime.UtcNow.AddMinutes(15);
        }
    }
}
