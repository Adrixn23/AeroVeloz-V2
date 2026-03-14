using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;
namespace AeroVeloz.Domain.Entities.Users.User;

/// <summary>
/// Entidad de dominio que representa un usuario del sistema AeroVeloz.
/// Cada usuario pertenece a una organización y tiene asignado un rol que determina sus permisos.
/// Incluye mecanismos de seguridad como bloqueo por intentos fallidos y registro de dirección IP.
/// </summary>
/// 


[Table("Users", Schema ="Identitys")]

public partial class User : BEntity<Guid>
{
    public string? nameUser { get; init; }

    public string? passwordHash { get; init; }
    public DateTime createAt { get; init ; }

    public bool isActive { get;init ; }

    public byte[]? ipAdress { get; init ;}

    public DateTime? lastLoginAt { get;init ; }

    public int? failedLoginAttempts { get;init ; }

    public DateTime? lockedUntil { get;init ; }

    public int idOrganization { get; init; }

    public short idRol { get; init; }
}

