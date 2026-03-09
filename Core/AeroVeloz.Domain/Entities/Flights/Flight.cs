

﻿using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Flights;

/// <summary>
/// Entidad de dominio que representa un vuelo dentro del sistema AeroVeloz.
/// Contiene la información esencial del vuelo: aerolínea, aeropuertos de origen y destino,
/// horario de salida programado y puertas de embarque.
/// </summary>
public partial class Flight: BEntity<short>
{
    public string? codeAirlines { get; init; }

    public byte flightStateId { get; init; }

    public string? OriginAirport { get; init; }

    public string? DestinationAirport { get; init; }

    public DateTimeOffset ScheduledDeparture { get; init; }

    public string? BordingGate { get; init; }


    public string? BoardingGateArrived { get; init; }
}
