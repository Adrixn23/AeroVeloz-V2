using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Organization.Base
{
    /// <summary>
    /// Clase base abstracta que representa una organización dentro del sistema AeroVeloz.
    /// Todas las organizaciones (aeropuertos, aerolíneas, etc.) heredan de esta clase
    /// y comparten propiedades comunes como nombre, tipo, correo electrónico y estado activo.
    /// </summary>
    public abstract class Organizations : BEntity<int>
    {
        public string? nameOrganization { get; init; }

        public  string? typeOrganization  { get; init; }

        public bool isActived { get; init; }

        public string? emailOrganization { get; init; }

        public DateTime createAt { get; init; }

    }
}
