using AeroVeloz.Domain.Entities.Organization.Base;

namespace AeroVeloz.Domain.Entities.Airlines;

public partial class Airline : Organizations
{
 
    public string? codeAirlines { get; init; }
    public string? codeIATA { get; init; }
}
