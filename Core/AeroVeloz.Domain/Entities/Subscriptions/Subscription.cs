
﻿using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Subscriptions;


/// <summary>
/// Entidad de dominio que representa una suscripción de un interesado a las actualizaciones de un vuelo.
/// Permite a los usuarios registrarse para recibir notificaciones sobre cambios en vuelos específicos
/// a través de un canal determinado (email, SMS, etc.) durante un período de tiempo definido.
/// </summary>

public partial class Subscription : BEntity<Guid>
{
   public short flightNumber { get; init; }
   public string? codeAirlines { get; init; }
   public byte codeChannel { get; init; }
   public int numberInterested { get; init; }
   public DateTime createDate { get; init; }
   public DateTime endingDate { get; init; }
   public bool activeSubscription { get; init; }
   public string? contactValue { get; init; }

}
