using AeroVeloz.Domain.Entities.Operations;
using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Security;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OperationChange> OperationChanges { get; set; } = new List<OperationChange>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
