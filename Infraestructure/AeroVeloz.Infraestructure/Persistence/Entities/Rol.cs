
namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Rol
{
    public short IdRol { get; set; }

    public string NameRol { get; set; } = null!;

    public virtual ICollection<RolPermission> RolPermissions { get; set; } = new List<RolPermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
