namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class RolPermission
{
    public short IdRolPermission { get; set; }

    public short IdRol { get; set; }

    public short IdPermission { get; set; }

    public virtual Permission IdPermissionNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
