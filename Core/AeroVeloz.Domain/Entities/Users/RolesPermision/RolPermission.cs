using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users.RolesPermision
{
    public class RolPermission : BEntity<short>
    {
        public short idRol { get; init; }
        public short idPermission { get; init; }    
    }
}
