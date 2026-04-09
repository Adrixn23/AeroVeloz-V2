using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.DTOs.Operations;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Operational;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Operations;
using AeroVeloz.Domain.Common.CodeErrors;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Events.Operations;
using AeroVeloz.Domain.Models.Operational;
using AeroVeloz.Domain.Validators.interfaces.Operations;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;
using System.Text.Json;

namespace AeroVeloz.Application.Handlers.Operations
{
    public class OperationalService : IOperationalService
    {
        private readonly IOperationalRepository _repo;
        private readonly IOperationalChangeValidator _validator;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IAuditRepository _auditRepo;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;
        private readonly IMediator _mediator;

        public OperationalService(
            IOperationalRepository repo,
            IOperationalChangeValidator validator,
            IUserRepositoryAuthorization auth,
            IAuditRepository auditRepo,
            IOrganizationMonitoringLogger monitoringLogger,
            IMediator mediator)
        {
            _repo = repo;
            _validator = validator;
            _auth = auth;
            _auditRepo = auditRepo;
            _monitoringLogger = monitoringLogger;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> RegisterAsync(OperationalChangeSaveDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyFlightAsync(userId, dto.FlightNumber, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var operation = new OperationChange
                {
                    Id = Guid.NewGuid(),
                    idUser = userId,
                    idOperationalType = dto.IdOperationalType,
                    flightNumber = dto.FlightNumber,
                    codeAirlinesIcao = dto.CodeAirline,
                    codeAirportIcao = dto.CodeAirport,
                    previosValue = dto.PreviousValue,
                    newValue = dto.NewValue,
                    cause = dto.Cause,
                    changeAt = DateTime.UtcNow,
                    isActive = true
                };

                var validation = await _validator.ValidateForCreateOperational(operation);
                if (!validation.IsValid)
                    return OperationResult<bool>.FromValidation(validation);

                var created = await _repo.CreateEntity(operation);
                if (!created)
                    return OperationResult<bool>.Fail("OP_PERSIST", "No se pudo registrar el cambio operacional");

                var operationalTypeName = await _repo.GetOperationalTypeNameAsync(dto.IdOperationalType);


                var values = JsonSerializer.Serialize(operation);
                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 1,
                    idUser = userId,
                    nameEntity = "OperationChange",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = values
                });


                var result = OperationResult<bool>.Ok(true, "Cambio operacional registrado");

                result.AddEvent(new OperationalChangeRegisteredDomainEvent(
                    operation.Id, userId, dto.FlightNumber, dto.CodeAirline,
                    dto.CodeAirport, operationalTypeName, dto.PreviousValue, dto.NewValue,
                    dto.Cause, DateTime.UtcNow));

                foreach (var evt in result.DomainEvents)
                    await _mediator.Publish(evt);

                return result;
            }
            catch (DatabaseOperationException ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.RegisterAsync",
                    Message = "Error de base de datos al registrar cambio operacional"
                }, ex);
                return OperationResult<bool>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.RegisterAsync",
                    Message = "Error inesperado al registrar cambio operacional"
                }, ex);
                return OperationResult<bool>.Fail("OP_ERROR", "Error inesperado al registrar el cambio operacional");
            }
        }

        public async Task<OperationResult<bool>> UpdateAsync(OperationalChangeUpdateDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOperationsAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var operation = new OperationChange
                {
                    Id = dto.Id,
                    idUser = userId,
                    idOperationalType = dto.IdOperationalType,
                    flightNumber = dto.FlightNumber,
                    codeAirlinesIcao = dto.CodeAirline,
                    codeAirportIcao = dto.CodeAirport,
                    previosValue = dto.PreviousValue,
                    newValue = dto.NewValue,
                    cause = dto.Cause,
                    changeAt = DateTime.UtcNow,
                    isActive = true
                };

                var validation = await _validator.ValidateForCreateOperational(operation);
                if (!validation.IsValid)
                    return OperationResult<bool>.FromValidation(validation);

                var updated = await _repo.UpdateEntity(operation);
                if (!updated)
                    return OperationResult<bool>.Fail("OP_UPDATE", "No se pudo actualizar el cambio operacional");

                var values = JsonSerializer.Serialize(operation);
                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 2,
                    idUser = userId,
                    nameEntity = "OperationChange",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = values
                });

                var result = OperationResult<bool>.Ok(true, "Cambio operacional actualizado");
                return result;
            }
            catch (DatabaseOperationException ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.UpdateAsync",
                    Message = "Error de base de datos al actualizar cambio operacional"
                }, ex);
                return OperationResult<bool>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.UpdateAsync",
                    Message = "Error inesperado al actualizar cambio operacional"
                }, ex);
                return OperationResult<bool>.Fail("OP_ERROR", "Error inesperado al actualizar el cambio operacional");
            }
        }

        public async Task<OperationResult<OperationalModel>> GetByIdAsync(Guid operationId, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);

                if (!authResult.IsValid)
                    return OperationResult<OperationalModel>.FromValidation(authResult);

                var operation = await _repo.GetByOperationAsync(operationId);

                return OperationResult<OperationalModel>.Ok(operation);

            }
            catch (DatabaseOperationException ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.GetByIdAsync",
                    Message = $"Error de base de datos al obtener operación: {operationId}"
                }, ex);
                return OperationResult<OperationalModel>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.GetByIdAsync",
                    Message = $"Error inesperado al obtener operación: {operationId}"
                }, ex);
                return OperationResult<OperationalModel>.Fail("OP_ERROR", "Error inesperado al obtener la operación");
            }
        }


        public async Task<OperationResult<bool>> DesactiveOperational(OperationalChangeRemoveDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOperationsAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var operation = await _repo.GetByOperationAsync(dto.IdOperational);

                if(operation == null)
                {
                    var vl = new ValidationResult().Failur(OperationalChangeErrors.OperationsNotExist);
                    return OperationResult<bool>.FromValidation(vl);
                }

                var op = new OperationChange { Id = dto.IdOperational, isActive = false };
                var deactivated = await _repo.DeleteEntity(op);

                if (!deactivated)
                    return OperationResult<bool>.Fail("OPERATION_DESACTIVE", "No se pudo desactivar la operacion");

                var newValues = JsonSerializer.Serialize(op);

                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 3,
                    idUser = userId,
                    nameEntity = "Operations",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = newValues,
                });

                var result = OperationResult<bool>.Ok(true, "Operation desactivada");
                return result;

            }
            catch (DatabaseOperationException ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.DesactiveOperational",
                    Message = $"Error de base de datos al desactivar la operacion: {dto.IdOperational}"
                }, ex);

                return OperationResult<bool>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.DeactivateAsync",
                    Message = $"Error inesperado al desactivar la operacion: {dto.IdOperational}"
                }, ex);

                return OperationResult<bool>.Fail("OPERATION_ERROR", "Error inesperado al desactivar la operacion");

            }

        }


        public async Task<OperationResult<IReadOnlyCollection<OperationalModel>>> GetFlightChangesAsync(
            short flightNumber, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<OperationalModel>>.FromValidation(authResult);

                var changes = await _repo.GetFlightChangesAsync(flightNumber);
                return OperationResult<IReadOnlyCollection<OperationalModel>>.Ok(changes);
            }
            catch (DatabaseOperationException ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.GetFlightChangesAsync",
                    Message = $"Error de base de datos al obtener cambios del vuelo: {flightNumber}"
                }, ex);
                return OperationResult<IReadOnlyCollection<OperationalModel>>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.GetFlightChangesAsync",
                    Message = $"Error inesperado al obtener cambios del vuelo: {flightNumber}"
                }, ex);
                return OperationResult<IReadOnlyCollection<OperationalModel>>.Fail("OP_ERROR", "Error inesperado al obtener cambios del vuelo");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<OperationalDetailModel>>> GetAirportChangesAsync(
            Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<OperationalDetailModel>>.FromValidation(authResult);

                var changes = await _repo.GetAirportChangesAsync(orgId);
                return OperationResult<IReadOnlyCollection<OperationalDetailModel>>.Ok(changes);
            }
            catch (DatabaseOperationException ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.GetAirportChangesAsync",
                    Message = "Error de base de datos al obtener cambios del aeropuerto"
                }, ex);
                return OperationResult<IReadOnlyCollection<OperationalDetailModel>>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "OperationalService.GetAirportChangesAsync",
                    Message = "Error inesperado al obtener cambios del aeropuerto"
                }, ex);
                return OperationResult<IReadOnlyCollection<OperationalDetailModel>>.Fail("OP_ERROR", "Error inesperado al obtener cambios del aeropuerto");
            }
        }

   

    }



}
