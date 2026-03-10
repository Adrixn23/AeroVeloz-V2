using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Flights;



/// <summary>
/// Entidad de dominio que representa un estado posible de un vuelo
/// (ej: "SCHEDULED", "BOARDING", "INFLIGHT", "LANDED", "CANCELLED").
/// </summary>
public partial class FlightState : BEntity<byte>
{
    public string? codeFlightState { get; init; }

    public string? name { get; init; }


}
