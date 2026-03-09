using AeroVeloz.Application.Contracts.Subscriptions;
using AeroVeloz.Application.DTOs.Subscriptions;
using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Application.Repositories.Subscriptions;
using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.DomainService.Interfaces.Subscriptions;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.Events.EventsSubscriptions;
using AeroVeloz.Domain.Validators.interfaces.Subscriptions;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionServicie
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IFlightRepository _flightRepo;
        private readonly ISubscriptionsDomainService _subscriptionDomain;
        private readonly ISubscriptionValidator _subscriptionValidator;
        private readonly IMediator _mediator;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepo,
            IFlightRepository flightRepo,
            ISubscriptionsDomainService subscriptionDomain,
            ISubscriptionValidator subscriptionValidator,
            IMediator mediator,
            IOrganizationMonitoringLogger monitoringLogger)
        {
            _subscriptionRepo = subscriptionRepo;
            _flightRepo = flightRepo;
            _subscriptionDomain = subscriptionDomain;
            _subscriptionValidator = subscriptionValidator;
            _mediator = mediator;
            _monitoringLogger = monitoringLogger;
        }

        public async Task<OperationResult<bool>> SubscribeExternalAsync(SubscriptionSaveDto dto)
        {
            try
            {
                var inputValidation = _subscriptionValidator.ValidateCreate(dto.FlightNumber, dto.CodeAirlines!, dto.CodeChannel, dto.ContactValue!);
                if (!inputValidation.IsValid)
                    return OperationResult<bool>.FromValidation(inputValidation);

                var flight = await _flightRepo.GetByFlightNumberAndAirlineAsync(dto.FlightNumber, dto.CodeAirlines!);
                if (flight == null)
                    return OperationResult<bool>.Fail("SUB_FLIGHT", "Vuelo no encontrado");

                var flightValid = await _subscriptionDomain.ValidateFlightAcceptsSubscriptionsAsync(dto.FlightNumber, dto.CodeAirlines!);
                if (!flightValid.IsValid)
                    return OperationResult<bool>.FromValidation(flightValid);

                var duplicate = await _subscriptionRepo.ExistsDuplicateAsync(dto.FlightNumber, dto.CodeAirlines!, dto.CodeChannel, dto.ContactValue!);
                if (duplicate)
                    return OperationResult<bool>.Fail("SUB_DUPLICATE", "Ya existe una suscripción activa con estos datos");

                var subscription = new Subscription
                {
                    Id = Guid.NewGuid(),
                    flightNumber = dto.FlightNumber,
                    codeAirlines = dto.CodeAirlines,
                    codeChannel = dto.CodeChannel,
                    contactValue = dto.ContactValue,
                    numberInterested = 1,
                    createDate = DateTime.UtcNow,
                    endingDate = flight.ScheduledDeparture.UtcDateTime.AddHours(24),
                    activeSubscription = true
                };

                var persisted = await _subscriptionRepo.CreateAsync(subscription);
                if (!persisted)
                    return OperationResult<bool>.Fail("SUB_PERSIST", "Error al crear la suscripción");

                var op = OperationResult<bool>.Ok(true, "Suscripción creada exitosamente");
                op.AddEvent(new SubscriptionCreated(
                    subscription.Id, dto.FlightNumber, dto.CodeAirlines!,
                    dto.ContactValue!, (Domain.Common.Enums.SubscriptionChannel)dto.CodeChannel, DateTime.UtcNow));

                foreach (var @event in op.DomainEvents)
                    await _mediator.Publish(@event);

                return op;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(SubscriptionService),
                    Message = $"Error al crear suscripción para vuelo {dto.FlightNumber}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<bool>.Fail("SUB_ERROR", "Error interno al crear la suscripción");
            }
        }

        public async Task<OperationResult<bool>> CancelSubscriptionAsync(Guid subscriptionId)
        {
            try
            {
                var cancelled = await _subscriptionRepo.CancelAsync(subscriptionId);
                if (!cancelled)
                    return OperationResult<bool>.Fail("SUB_CANCEL", "Suscripción no encontrada o ya cancelada");

                var op = OperationResult<bool>.Ok(true, "Suscripción cancelada");
                op.AddEvent(new SubscriptionCancelled(subscriptionId, 0, DateTime.UtcNow, string.Empty, "User requested cancellation"));

                foreach (var @event in op.DomainEvents)
                    await _mediator.Publish(@event);

                return op;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(SubscriptionService),
                    Message = $"Error al cancelar suscripción {subscriptionId}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<bool>.Fail("SUB_ERROR", "Error interno al cancelar la suscripción");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<SubscriptionReadDto>>> GetByFlightAsync(short flightNumber, string codeAirlines)
        {
            try
            {
                var subs = await _subscriptionRepo.GetSubscriptionsByFlightAsync(flightNumber, codeAirlines);
                return OperationResult<IReadOnlyCollection<SubscriptionReadDto>>.Ok(subs);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(SubscriptionService),
                    Message = $"Error al consultar suscripciones del vuelo {flightNumber}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<IReadOnlyCollection<SubscriptionReadDto>>.Fail("QUERY_ERROR", "Error al consultar suscripciones");
            }
        }

        public async Task<OperationResult<int>> GetInterestedCountAsync(short flightNumber, string codeAirlines)
        {
            try
            {
                var count = await _subscriptionRepo.GetInterestedCountAsync(flightNumber, codeAirlines);
                return OperationResult<int>.Ok(count);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(SubscriptionService),
                    Message = $"Error al contar interesados en vuelo {flightNumber}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<int>.Fail("QUERY_ERROR", "Error al consultar interesados");
            }
        }
    }
}
