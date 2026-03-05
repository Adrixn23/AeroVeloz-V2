using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class UserOrganizaton
{
    public int IdUserOrganization { get; set; }

    public int IdOrganizations { get; set; }

    public short IdRol { get; set; }

    public Guid IdUser { get; set; }

    public virtual Organization IdOrganizationsNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
