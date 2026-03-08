using AeroVeloz.Domain.Entities.Organization.Base;

namespace AeroVeloz.Domain.Entities.Airlines;

/// <summary>
/// Entidad de dominio que representa una aerolínea registrada en el sistema.
/// utilizados para identificarla en operaciones de vuelo y conexiones con aeropuertos.
/// </summary>
public partial class Airline : Organizations
{
    public string? codeAirlines { get; init; }

    public string? codeIATA { get; init; }
}
