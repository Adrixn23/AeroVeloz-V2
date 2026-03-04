using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Permission
{
    public short IdPermission { get; set; }

    public string CodePermission { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<RolPermission> RolPermissions { get; set; } = new List<RolPermission>();
}
