using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Application.Repositories.Subscriptions;
using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.DomainService.Interfaces.Flights;
using AeroVeloz.Domain.Entities.Audit;

using AeroVeloz.Domain.Events.EventsAirlines;
using AeroVeloz.Domain.Events.EventsFlights;
using AeroVeloz.Domain.Validators.interfaces.Flight;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.Services.Flights
{
    public class FlightService : IFlightServicie
    {
        private readonly IFlightRepository _flightRepo;
        private readonly IFlightDomainService _flightDomain;
        private readonly IFlightValidator _flightValidator;
        private readonly IUserRepositoryAuthorization _authzRepo;
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IAuditWriteRepository _auditRepo;
        private readonly IMediator _mediator;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;

        public FlightService(
            IFlightRepository flightRepo,
            IFlightDomainService flightDomain,
            IFlightValidator flightValidator,
            IUserRepositoryAuthorization authzRepo,
            ISubscriptionRepository subscriptionRepo,
            IAuditWriteRepository auditRepo,
            IMediator mediator,
            IOrganizationMonitoringLogger monitoringLogger)
        {
            _flightRepo = flightRepo;
            _flightDomain = flightDomain;
            _flightValidator = flightValidator;
            _authzRepo = authzRepo;
            _subscriptionRepo = subscriptionRepo;
            _auditRepo = auditRepo;
            _mediator = mediator;
            _monitoringLogger = monitoringLogger;
        }

        public async Task<OperationResult<FlightBatchResultDto>> UploadBatchAsync(
            IEnumerable<FlightBatchItemDto> batch, Guid userId, int orgId)
        {
            try
            {
                var isAirline = await _authzRepo.IsAirlineAdminAsync(userId, orgId);
                if (!isAirline)
                    return OperationResult<FlightBatchResultDto>.Fail("BATCH_AUTH", "Solo administradores de aerolínea pueden cargar vuelos");

                var errors = new List<FlightBatchErrorDto>();
                var validFlights = new List<Domain.Entities.Flights.Flight>();
                var items = batch.ToList();
                
                if (items.Count == 0)
                    return OperationResult<FlightBatchResultDto>.Fail("BATCH_EMPTY", "El lote no contiene vuelos");

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    var flight = new Domain.Entities.Flights.Flight
                    {
                        codeAirlinesIcao = item.CodeAirlines,
                        flightStatesId = (byte)FlightStateEnum.Scheduled,
                        OriginAirport = item.OriginAirport,
                        DestinationAirport = item.DestinationAirport,
                        ScheduledDeparture = item.ScheduledDeparture,
                        BordingGate = item.BoardingGate,
                        BoardingGateArrived = item.BoardingGateArrived
                    };

                    var rowValidation = await _flightValidator.ValidateFlightRowAsync(flight);
                    if (!rowValidation.IsValid)
                    {
                        foreach (var err in rowValidation.domainErrors)
                            errors.Add(new FlightBatchErrorDto(i + 1, item.CodeAirlines, err.code, err.description));
                        continue;
                    }

                    var exists = await _flightRepo.ExistsFlightAsync(flight.Id, item.CodeAirlines!);
                    if (exists)
                    {
                        errors.Add(new FlightBatchErrorDto(i + 1, item.CodeAirlines, "FLIGHT_DUPLICATE", "El vuelo ya existe en el sistema"));
                        continue;
                    }

                    var originCheck = await _flightDomain.IsValidOriginAirportAsync(item.CodeAirlines!, item.OriginAirport!);
                    if (!originCheck.IsValid)
                    {
                        foreach (var err in originCheck.domainErrors)
                            errors.Add(new FlightBatchErrorDto(i + 1, item.CodeAirlines, err.code, err.description));
                        continue;
                    }

                    var destCheck = await _flightDomain.IsValidDestinationAirportAsync(item.CodeAirlines!, item.DestinationAirport!);
                    if (!destCheck.IsValid)
                    {
                        foreach (var err in destCheck.domainErrors)
                            errors.Add(new FlightBatchErrorDto(i + 1, item.CodeAirlines, err.code, err.description));
                        continue;
                    }

                    validFlights.Add(flight);
                }

                if (validFlights.Count > 0)
                {
                    await _flightRepo.PersistBatchAsync((IEnumerable<Domain.Entities.Flights.Flight>)validFlights);

                    foreach (var f in validFlights)
                        await _subscriptionRepo.AutoSubscribeAirlineAsync(f.Id, f.codeAirlinesIcao!, orgId);
                }

                var result = new FlightBatchResultDto(items.Count, validFlights.Count, errors.Count, errors);
                var op = OperationResult<FlightBatchResultDto>.Ok(result, $"{validFlights.Count} vuelos persistidos, {errors.Count} rechazados");

                if (validFlights.Count > 0)
                    op.AddEvent(new FlightBatchProcessed(items[0].CodeAirlines!, validFlights.Count, DateTime.UtcNow));
                if (errors.Count > 0)
                    op.AddEvent(new FlightBatchRejected(items[0].CodeAirlines!, $"{errors.Count} vuelos rechazados", DateTime.UtcNow));

                foreach (var @event in op.DomainEvents)
                    await _mediator.Publish(@event);

                return op;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = "Error al procesar lote de vuelos",
                    Detail = ex.Message,
                    UserId = userId,
                    OrganizationId = orgId,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<FlightBatchResultDto>.Fail("BATCH_ERROR", "Error interno al procesar el lote de vuelos");
            }
        }

        public async Task<OperationResult<FlightBatchResultDto>> UploadCsvAsync(
            Stream csvStream, Guid userId, int orgId, ICsvFlightParser parser)
        {
            try
            {
                var items = parser.Parse(csvStream, out var parseErrors);
                if (parseErrors.Count > 0 && items.Count == 0)
                {
                    var csvErrors = parseErrors.Select((e, i) => new FlightBatchErrorDto(i + 1, null, "CSV_PARSE", e)).ToList();
                    return OperationResult<FlightBatchResultDto>.Ok(
                        new FlightBatchResultDto(0, 0, csvErrors.Count, csvErrors), "Archivo CSV no contiene registros válidos");
                }

                return await UploadBatchAsync(items, userId, orgId);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = "Error al leer archivo CSV",
                    Detail = ex.Message,
                    UserId = userId,
                    OrganizationId = orgId,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<FlightBatchResultDto>.Fail("CSV_ERROR", "Error al procesar el archivo CSV");
            }
        }

        public async Task<OperationResult<bool>> UpdateStateAsync(FlightUpdateStateDto dto, Guid userId, int orgId)
        {
            try
            {
                var authz = await _authzRepo.CanModifyFlightAsync(userId, dto.FlightNumber, orgId);
                if (!authz.IsValid)
                    return OperationResult<bool>.FromValidation(authz);

                var flight = await _flightRepo.GetEntityByNumberAndAirlineAsync(dto.FlightNumber, dto.CodeAirlines!);
                if (flight == null)
                    return OperationResult<bool>.Fail("FLIGHT_NOT_FOUND", "Vuelo no encontrado");

                            var sameState = _flightValidator.ValidateStateTransition(flight.flightStatesId, dto.NewFlightStateId);
                if (!sameState.IsCompleted)
                    return OperationResult<bool>.FromValidation(await sameState);

                var transition = await _flightDomain.IsValidStatusTransitionAsync(flight.flightStatesId, (FlightStateEnum)dto.NewFlightStateId);
                if (!transition.IsValid)
                    return OperationResult<bool>.FromValidation(transition);

                var updated = await _flightRepo.UpdateFlightStateAsync(dto.FlightNumber, dto.CodeAirlines!, dto.NewFlightStateId);
                if (!updated)
                    return OperationResult<bool>.Fail("FLIGHT_UPDATE", "No se pudo actualizar el estado del vuelo");

                await _auditRepo.RegisterAuditAsync(new Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 1,
                    nameEntity = "Flight",
                    ocurrentAt = DateTime.UtcNow,
                    idUser = userId,
                    DataOld = $"{{\"flightStateId\":{flight.flightStatesId}}}",
                    DataNew = $"{{\"flightStateId\":{dto.NewFlightStateId},\"reason\":\"{dto.Reason}\"}}"
                });

                if (dto.NewFlightStateId == (byte)FlightStateEnum.Completed ||
                    dto.NewFlightStateId == (byte)FlightStateEnum.Cancelled)
                {
                    await _subscriptionRepo.CloseAllForFlightAsync(dto.FlightNumber, dto.CodeAirlines!);
                }

                var op = OperationResult<bool>.Ok(true, "Estado actualizado");
                op.AddEvent(new FlightStateChangedByAirline(
                    dto.FlightNumber.ToString(), (short)(FlightStateEnum)dto.NewFlightStateId, dto.CodeAirlines!, DateTime.UtcNow));
                op.AddEvent(new FlightAuditEntryCreated(
                    Guid.NewGuid(), "Airline", $"Flight {dto.FlightNumber} state -> {dto.NewFlightStateId}",
                    $"{{\"reason\":\"{dto.Reason}\"}}", DateTime.UtcNow));

                if (dto.NewFlightStateId == (byte)FlightStateEnum.Delayed)
                {
                    op.AddEvent(new FlightDelayed(
                        dto.FlightNumber, dto.CodeAirlines!,
                        (FlightStateEnum)flight.flightStatesId, flight.ScheduledDeparture, DateTime.UtcNow));
                }

                foreach (var @event in op.DomainEvents)
                    await _mediator.Publish(@event);

                return op;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = $"Error al actualizar estado del vuelo {dto.FlightNumber}",
                    Detail = ex.Message,
                    UserId = userId,
                    OrganizationId = orgId,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<bool>.Fail("STATE_ERROR", "Error interno al actualizar el estado del vuelo");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<FlightReadDto>>> GetFlightsByAirlineAsync(string codeAirlines, int orgId)
        {
            try
            {
                var flights = await _flightRepo.GetActiveFlightsByAirlineAsync(codeAirlines);
                return OperationResult<IReadOnlyCollection<FlightReadDto>>.Ok(flights);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = $"Error al consultar vuelos de aerolínea {codeAirlines}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<IReadOnlyCollection<FlightReadDto>>.Fail("QUERY_ERROR", "Error al consultar vuelos");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<FlightReadDto>>> GetPublicActiveFlightsAsync()
        {
            try
            {
                var flights = await _flightRepo.GetPublicActiveFlightsAsync();
                return OperationResult<IReadOnlyCollection<FlightReadDto>>.Ok(flights);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = "Error al consultar vuelos públicos activos",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<IReadOnlyCollection<FlightReadDto>>.Fail("QUERY_ERROR", "Error al consultar vuelos");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<FlightReadDto>>> GetPublicFlightsByAirportAsync(string airportCode)
        {
            try
            {
                var flights = await _flightRepo.GetPublicFlightsByAirportAsync(airportCode);
                return OperationResult<IReadOnlyCollection<FlightReadDto>>.Ok(flights);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = $"Error al consultar vuelos del aeropuerto {airportCode}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<IReadOnlyCollection<FlightReadDto>>.Fail("QUERY_ERROR", "Error al consultar vuelos");
            }
        }

        public async Task<OperationResult<FlightReadDto>> GetFlightDetailAsync(short flightNumber, string codeAirlines)
        {
            try
            {
                var flight = await _flightRepo.GetByFlightNumberAndAirlineAsync(flightNumber, codeAirlines);
                if (flight == null)
                    return OperationResult<FlightReadDto>.Fail("FLIGHT_NOT_FOUND", "Vuelo no encontrado");
                return OperationResult<FlightReadDto>.Ok(flight);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightService),
                    Message = $"Error al consultar detalle vuelo {flightNumber}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<FlightReadDto>.Fail("QUERY_ERROR", "Error al consultar vuelo");
            }
        }
    }
}
