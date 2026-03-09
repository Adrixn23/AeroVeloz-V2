using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users.Roles
{
    /// <summary>
    /// Entidad de dominio que representa un rol dentro del sistema.
    /// Los roles determinan el nivel de acceso y las acciones que un usuario puede realizar
    /// (ej: SYSTEMADMIN, AIRPORTADMIN, AIRLINEADMIN).
    /// </summary>
    public partial class Roles : BEntity<short>
    {
        public string? nameRol { get; init; }
    }
}
