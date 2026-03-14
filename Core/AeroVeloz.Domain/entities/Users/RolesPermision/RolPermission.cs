using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Users.RolesPermision
{
    /// <summary>
    /// Entidad de dominio que representa la relación muchos-a-muchos entre roles y permisos.
    /// permitiendo configurar qué permisos tiene cada rol dentro del sistema.
    /// </summary>
    /// 

    [Table("RolPermissions", Schema ="Identitys")]

    public class RolPermission
    {
        [Key]
        public short idRolPermission { get; init; }

        public short idRol { get; init; }

        public short idPermission { get; init; }    
    }
}
