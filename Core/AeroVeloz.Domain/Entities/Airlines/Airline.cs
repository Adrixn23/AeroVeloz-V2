using AeroVeloz.Domain.Entities.Organization.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Airlines;


/// Entidad de dominio que representa una aerolínea registrada en el sistema.
/// utilizados para identificarla en operaciones de vuelo y conexiones con aeropuertos.


[Table("Airlines", Schema = "Flights")]

public partial class Airline : Organizations
{
    [Column("codeAirlines")]
    public string? codeAirlinesIcao { get; init; }

    [Column("codeIATA")]
    public string? codeIata { get; init; }
}
