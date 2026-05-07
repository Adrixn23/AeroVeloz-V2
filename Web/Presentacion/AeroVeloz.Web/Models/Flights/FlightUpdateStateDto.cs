using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Web.Models.Flights
{
    public class FlightUpdateStateDto
    {
        [Required]
        public short FlightNumber { get; set; }
        
        [Required]
        public string CodeAirlinesIcao { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Debe seleccionar un estado válido")]
        public byte FlightStateId { get; set; }
        
        [Required(ErrorMessage = "La justificación es obligatoria")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "El motivo debe tener entre 10 y 50 caracteres para ser auditado.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,áéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El motivo no puede contener caracteres extraños (solo letras, números y puntuación básica).")]
        public string Reason { get; set; } = string.Empty;
    }
}
