using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Organization.Base { 
    /// <summary>
    /// Clase  que representa una organización dentro del sistema AeroVeloz.
    /// Todas las organizaciones (aeropuertos, aerolíneas, etc.) heredan de esta clase
    /// y comparten propiedades comunes como nombre, tipo, correo electrónico y estado activo,
    /// elemento usado como composicion
    /// </summary>
    /// 


    [Table("Organizations", Schema ="Identitys")]

    public class Organizations : BEntity<int>
    {
        public string? nameOrganization { get; init; }

        public  string? typeOrganization  { get; init; }

        public bool isActived { get; init; }

        public string? emailOrganization { get; init; }

        public DateTime createAt { get; init; }

    }
}
