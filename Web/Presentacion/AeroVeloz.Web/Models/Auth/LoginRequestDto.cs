using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Web.Models.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "El correo electrónico de la organización es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string EmailOrganization { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 50 caracteres.")]
        public string NameUser { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
