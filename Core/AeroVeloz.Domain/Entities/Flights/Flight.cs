using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Entities.Subscriptions;
using System.ComponentModel.DataAnnotations.Schema;
namespace AeroVeloz.Domain.Entities.Flights;

/// <summary>
/// Entidad de dominio que representa un vuelo dentro del sistema AeroVeloz.
/// Contiene la información esencial del vuelo: aerolínea, aeropuertos de origen y destino,
/// horario de salida programado y puertas de embarque.
/// </summary>
/// 


[Table("Flight", Schema ="Flights")]

public partial class Flight: BEntity<short>
{
    public string? codeAirlinesIcao { get; init; }

    public byte flightStatesId { get; init; }

    public string? OriginAirport { get; init; }

    public string? DestinationAirport { get; init; }

    public DateTimeOffset ScheduledDeparture { get; init; }

    public string? BordingGate { get; init; }

    public string? BoardingGateArrived { get; init; }
}
