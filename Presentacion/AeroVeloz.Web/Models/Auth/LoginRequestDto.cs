using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Web.Models.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "El correo electrónico de la organización es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string EmailOrganization { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string NameUser { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
