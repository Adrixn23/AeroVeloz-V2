using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Airlines
{
    public class AirlineReadDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("codeAirlinesIcao")]
        public string CodeAirlinesIcao { get; set; } = string.Empty;

        [JsonPropertyName("codeIata")]
        public string CodeIata { get; set; } = string.Empty;

        [JsonPropertyName("nameOrganization")]
        public string NameOrganization { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createAt")]
        public DateTime CreateAt { get; set; }
    }

    public class AirlineSaveDto
    {
        [Required(ErrorMessage = "El código ICAO es obligatorio (Ej: AVX).")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El código ICAO debe tener 3 caracteres.")]
        [JsonPropertyName("codeAirlinesIcao")]
        public string CodeAirlinesIcao { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código IATA es obligatorio (Ej: AV).")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El código IATA debe tener 2 caracteres.")]
        [JsonPropertyName("codeIata")]
        public string CodeIata { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de la organización es obligatorio.")]
        [JsonPropertyName("nameOrganization")]
        public string NameOrganization { get; set; } = string.Empty;
    }
}
