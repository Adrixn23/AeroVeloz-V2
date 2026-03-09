using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users.Permission
{


    /// Entidad de dominio que representa un permiso granular del sistema.
    /// Los permisos se asignan a los roles mediante la entidad
    /// y determinan las acciones específicas que un usuario puede ejecutar.
    

    public partial class Permission : BEntity<byte>
    {
        public string? codePermision { get; init; }

        public string? description { get; init; }
    }
}
