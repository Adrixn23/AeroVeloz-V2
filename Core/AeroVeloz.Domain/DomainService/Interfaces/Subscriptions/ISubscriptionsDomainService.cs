using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.Common.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.DomainService.Interfaces.Subscriptions
{
    public interface ISubscriptionsDomainService
    {
        // 1. Proceso de suscripcion (Valida Policy mas Guarda mas Dispara Evento)
        Task<ValidationResult> SubscribePassengerAsync(short flightNumber, string codeAirlines, SubscriptionChannel channel, string contactValue);

        // 2. Proceso de cancelación (Valida Policy mas Actualiza mas Dispara Evento)
        // Aquí no se necesita la aerolínea porque el Guid de la suscripción ya es único xd
        Task<ValidationResult> CancelSubscriptionAsync(Guid subscriptionId);

        // 3. Consultar suscripciones activas por vuelo (Para el Dispatcher de notificaciones)
        Task<IEnumerable<Subscription>> GetActiveSubscriptionsByFlightAsync(short flightNumber, string codeAirlines);

        // 4. Cierre automático por estado de vuelo cumple con el sad
        Task<ValidationResult> CloseAllSubscriptionsForFlightAsync(short flightNumber, string codeAirlines);
    }
}