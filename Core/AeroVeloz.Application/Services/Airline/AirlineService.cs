using AeroVeloz.Application.Contracts.Airlines;
using AeroVeloz.Application.DTOs.Airlines;
using AeroVeloz.Application.Repositories.Airlines;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Audit;
using AeroVeloz.Domain.Models.Airline;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;
using System.Text.Json;

namespace AeroVeloz.Application.Services.Airlines
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _repo;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;
        private readonly IAuditWriteRepository _auditRepo;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IMediator _mediator;

        public AirlineService(
            IAirlineRepository repo,
            IOrganizationMonitoringLogger monitoringLogger,
            IAuditWriteRepository auditRepo,
            IUserRepositoryAuthorization auth,
            IMediator mediator)
        {
            _repo = repo;
            _monitoringLogger = monitoringLogger;
            _auditRepo = auditRepo;
            _auth = auth;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> CreateAirlineAsync(AirlineSaveDto dto, Guid userId, int orgId)
        {
            try
            {
                var isAdmin = await _auth.IsAirlineAdminAsync(userId, orgId);
                if (!isAdmin)
                    return OperationResult<bool>.Fail("AIRLINE_AUTH", "No tiene permisos para crear aerolíneas");

                var exists = await _repo.ExistsByCodeAsync(dto.CodeAirlinesIcao);
                if (exists)
                    return OperationResult<bool>.Fail("AIRLINE_DUPLICATE", $"La aerolínea con código {dto.CodeAirlinesIcao} ya existe");

                var airline = new Airline
                {
                    codeAirlinesIcao = dto.CodeAirlinesIcao,
                    codeIata = dto.CodeIata,
                    nameOrganization = dto.NameOrganization,
                    typeOrganization = "AIRLINE",
                    isActived = true,
                    createAt = DateTime.UtcNow
                };

                var persisted = await _repo.CreateEntity(airline);
                if (!persisted)
                    return OperationResult<bool>.Fail("AIRLINE_PERSIST", "Error al guardar la aerolínea");

                await _auditRepo.RegisterAuditAsync(new Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 1, // Create
                    nameEntity = "Airline",
                    ocurrentAt = DateTime.UtcNow,
                    idUser = userId,
                    DataNew = JsonSerializer.Serialize(airline)
                });

                var op = OperationResult<bool>.Ok(true, "Aerolínea creada exitosamente");
                
                // Publicar eventos si existieran
                foreach (var @event in op.DomainEvents)
                    await _mediator.Publish(@event);

                return op;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = "AirlineService.CreateAirlineAsync",
                    Message = "Error al crear aerolínea",
                    Detail = ex.Message,
                    UserId = userId,
                    OrganizationId = orgId,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<bool>.Fail("AIRLINE_ERROR", "Error interno al procesar la creación");
            }
        }

        public async Task<OperationResult<bool>> UpdateAirlineAsync(AirlineSaveDto dto, string codeAirlinesIcao, Guid userId, int orgId)
        {
            try
            {
                var isAdmin = await _auth.IsAirlineAdminAsync(userId, orgId);
                if (!isAdmin)
                    return OperationResult<bool>.Fail("AIRLINE_AUTH", "No tiene permisos para modificar aerolíneas");

                var airline = await _repo.GetEntityByCodeAsync(codeAirlinesIcao);
                if (airline == null)
                    return OperationResult<bool>.Fail("AIRLINE_NOT_FOUND", "Aerolínea no encontrada");

                string oldData = JsonSerializer.Serialize(airline);

                var updatedAirline = new Airline
                {
                    Id = airline.Id,
                    codeAirlinesIcao = airline.codeAirlinesIcao,
                    codeIata = dto.CodeIata,
                    nameOrganization = dto.NameOrganization,
                    typeOrganization = airline.typeOrganization,
                    isActived = airline.isActived,
                    createAt = airline.createAt
                };

                var updated = await _repo.UpdateEntity(updatedAirline);
                if (!updated)
                    return OperationResult<bool>.Fail("AIRLINE_UPDATE", "No se pudo actualizar la aerolínea");

                await _auditRepo.RegisterAuditAsync(new Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 2, // Update
                    nameEntity = "Airline",
                    ocurrentAt = DateTime.UtcNow,
                    idUser = userId,
                    DataOld = oldData,
                    DataNew = JsonSerializer.Serialize(updatedAirline)
                });

                return OperationResult<bool>.Ok(true, "Aerolínea actualizada correctamente");
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = "AirlineService.UpdateAirlineAsync",
                    Message = $"Error al actualizar aerolínea {codeAirlinesIcao}",
                    Detail = ex.Message,
                    UserId = userId,
                    OrganizationId = orgId,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<bool>.Fail("AIRLINE_ERROR", "Error interno al actualizar");
            }
        }

        public async Task<OperationResult<bool>> DeleteAirlineAsync(string codeAirlinesIcao, Guid userId, int orgId)
        {
            try
            {
                var isAdmin = await _auth.IsAirlineAdminAsync(userId, orgId);
                if (!isAdmin)
                    return OperationResult<bool>.Fail("AIRLINE_AUTH", "No tiene permisos para eliminar aerolíneas");

                var airline = await _repo.GetEntityByCodeAsync(codeAirlinesIcao);
                if (airline == null)
                    return OperationResult<bool>.Fail("AIRLINE_NOT_FOUND", "Aerolínea no encontrada");

                var deleted = await _repo.DeleteEntity(airline);
                if (!deleted)
                    return OperationResult<bool>.Fail("AIRLINE_DELETE", "No se pudo realizar el borrado lógico");

                await _auditRepo.RegisterAuditAsync(new Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 3, // Delete
                    nameEntity = "Airline",
                    ocurrentAt = DateTime.UtcNow,
                    idUser = userId,
                    DataOld = JsonSerializer.Serialize(airline),
                    DataNew = "{\"isActived\": false}"
                });

                return OperationResult<bool>.Ok(true, "Aerolínea desactivada correctamente");
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = "AirlineService.DeleteAirlineAsync",
                    Message = $"Error al eliminar aerolínea {codeAirlinesIcao}",
                    Detail = ex.Message,
                    UserId = userId,
                    OrganizationId = orgId,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<bool>.Fail("AIRLINE_ERROR", "Error interno al eliminar");
            }
        }

        public async Task<OperationResult<AirlineDetailModel>> GetAirlineByCodeAsync(string codeAirlinesIcao)
        {
            try
            {
                var detail = await _repo.GetDetailByCodeAsync(codeAirlinesIcao);
                if (detail == null)
                    return OperationResult<AirlineDetailModel>.Fail("AIRLINE_NOT_FOUND", "Aerolínea no encontrada");

                return OperationResult<AirlineDetailModel>.Ok(detail);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = "AirlineService.GetAirlineByCodeAsync",
                    Message = $"Error al obtener aerolínea {codeAirlinesIcao}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<AirlineDetailModel>.Fail("QUERY_ERROR", "Error al consultar la aerolínea");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<AirlineDetailModel>>> GetAllActiveAirlinesAsync()
        {
            try
            {
                var airlines = await _repo.GetAllActiveDetailsAsync();
                return OperationResult<IReadOnlyCollection<AirlineDetailModel>>.Ok(airlines);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = "AirlineService.GetAllActiveAirlinesAsync",
                    Message = "Error al obtener lista de aerolíneas",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
                return OperationResult<IReadOnlyCollection<AirlineDetailModel>>.Fail("QUERY_ERROR", "Error al consultar aerolíneas");
            }
        }
    }
}