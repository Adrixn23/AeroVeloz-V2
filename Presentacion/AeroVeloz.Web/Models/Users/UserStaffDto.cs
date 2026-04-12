using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Users
{
    public class UserStaffDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nameUser")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("idRol")]
        public short RoleId { get; set; }
    }

    public class CreateStaffDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El usuario debe tener entre 5 y 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "El usuario solo puede contener letras, números y guiones bajos.")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña temporal es obligatoria.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres.")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("organizationId")]
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol válido.")]
        [JsonPropertyName("roleId")]
        public short RoleId { get; set; }
    }
}
