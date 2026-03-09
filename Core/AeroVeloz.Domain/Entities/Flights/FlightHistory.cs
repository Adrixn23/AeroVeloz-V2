namespace AeroVeloz.Domain.Entities.Flights;

public partial class FlightHistory
{
    public short flightNumber { get; init; }
    public short codeAirlines { get; init; }
    public DateTime changeAt { get; init; }
    public string? reason { get; init; }
    public short flightStatesIdAfter { get; init; }
    public short flightStatesIdBefore { get; init; }

/// <summary>
/// Entidad de dominio que registra el historial de cambios de estado de un vuelo.
/// Cada registro captura la transición de un estado a otro, incluyendo la razón del cambio
/// y la fecha en que ocurrió. Permite trazabilidad completa sobre la evolución de cada vuelo.
/// </summary>

}