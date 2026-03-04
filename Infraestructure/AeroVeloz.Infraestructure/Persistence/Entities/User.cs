using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class User
{
    public Guid IdUser { get; set; }

    public string NameUser { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LockedUntil { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public byte[]? IpAdress { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<OperationChange> OperationChanges { get; set; } = new List<OperationChange>();

    public virtual ICollection<UserOrganizaton> UserOrganizatons { get; set; } = new List<UserOrganizaton>();
}
