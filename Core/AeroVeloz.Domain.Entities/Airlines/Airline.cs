using AeroVeloz.Domain.Entities.Organization.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Airlines;

[Table("Airlines", Schema = "Flights")]
public partial class Airline : Organizations
{
    [Column("codeAirlinesIcao")]
    public string? codeAirlinesIcao { get; init; }

    [Column("codeIata")]
    public string? codeIata { get; init; }
}
