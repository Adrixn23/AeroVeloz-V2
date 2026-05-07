using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Events.Aiport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Validators.interfaces.Airport;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.Handlers.Airport
{
    public class AirportConnectionService : IAirportConnectionService
    {
        private readonly IAirportConnectionAirline _repo;
        private readonly IConnectionAiportAirline _validator;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;
        private readonly IMediator _mediator;
        private readonly IAirportRepository _airportRepo;

        public AirportConnectionService(
            IAirportConnectionAirline repo,
            IConnectionAiportAirline validator,
            IUserRepositoryAuthorization auth,
            IOrganizationMonitoringLogger monitoringLogger,
            IMediator mediator,
            IAirportRepository airportRepo)
        {
            _repo = repo;
            _validator = validator;
            _auth = auth;
            _monitoringLogger = monitoringLogger;
            _mediator = mediator;
            _airportRepo = airportRepo;
        }

        public async Task<OperationResult<bool>> CreateConnectionAsync(ConnectionAirlineByAirportSaveDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

               
                var connection = new ConectionsAirlineAirport
                {
                    Id = Guid.NewGuid(),
                    codeAirlinesIcao = dto.codeAirlinesIcao,
                    codeAirportIcao = dto.codeAirportIcao,
                    isActive = true,
                    createAt = DateTime.UtcNow
                };
            

                var validation = await _validator.ValidationForCreateConnectionAirlineByAirport(connection);
                if (!validation.IsValid)
                    return OperationResult<bool>.FromValidation(validation);

                var created = await _repo.CreateEntity(connection);
                if (!created)
                    return OperationResult<bool>.Fail("CONN_PERSIST", "No se pudo crear la conexión");


                var result = OperationResult<bool>.Ok(true, "Conexión creada exitosamente");
                result.AddEvent(new AirportConnectionCreatedDomainEvent(
                    connection.Id, dto.codeAirportIcao, dto.codeAirlinesIcao, userId, DateTime.UtcNow));

                foreach (var evt in result.DomainEvents)
                    await _mediator.Publish(evt);

                return result;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportConnectionService.CreateConnectionAsync",
                    Message = "Error inesperado al crear conexión aeropuerto-aerolínea"
                }, ex);
                return OperationResult<bool>.Fail("CONN_ERROR", "Error inesperado al crear la conexión");
            }
        }

        public async Task<OperationResult<bool>> UpdateConnectionAsync(ConnectionAirlineByAirportUpdateDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var airport = await _airportRepo.GetAirportByCode(dto.codeAirportIcao);
                if (airport == null)
                    return OperationResult<bool>.Fail("AIRPORT_NOT_FOUND", "El aeropuerto especificado no existe");

                var connection = new ConectionsAirlineAirport
                {
                    Id = dto.Id,
                    codeAirlinesIcao = dto.codeAirlinesIcao,
                    codeAirportIcao = dto.codeAirportIcao,
                    isActive = dto.isActive,
                    createAt = DateTime.UtcNow
                };

                var validation = await _validator.ValidationForUpdateConnectionAirlineByAirport(connection);
                if (!validation.IsValid)
                    return OperationResult<bool>.FromValidation(validation);

                var updated = await _repo.UpdateEntity(connection);
                if (!updated)
                    return OperationResult<bool>.Fail("CONN_UPDATE", "No se pudo actualizar la conexión");

                var result = OperationResult<bool>.Ok(true, "Conexión actualizada exitosamente");
                return result;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportConnectionService.UpdateConnectionAsync",
                    Message = "Error inesperado al actualizar conexión aeropuerto-aerolínea"
                }, ex);
                return OperationResult<bool>.Fail("CONN_ERROR", "Error inesperado al actualizar la conexión");
            }
        }

        public async Task<OperationResult<bool>> DeactivateConnectionAsync(Guid connectionId, string airportIcao,  Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);


                var allConnections = await _repo.GetAirportConnectionById(airportIcao);
                var targetConn = allConnections.FirstOrDefault();


                var connection = new ConectionsAirlineAirport { Id = connectionId, isActive = false };
                var deactivated = await _repo.DeleteEntity(connection);

                if (!deactivated)
                    return OperationResult<bool>.Fail("CONN_DEACTIVATE", "No se pudo desactivar la conexión");



                var result = OperationResult<bool>.Ok(true, "Conexión desactivada");
                result.AddEvent(new AirportConnectionDeactivatedDomainEvent(
                    connectionId, targetConn?.airportCode ?? "N/A", targetConn?.airlineCode ?? "N/A", userId, DateTime.UtcNow));

                foreach (var evt in result.DomainEvents)
                    await _mediator.Publish(evt);

                return result;
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportConnectionService.DeactivateConnectionAsync",
                    Message = $"Error inesperado al desactivar conexión: {connectionId}"
                }, ex);
                return OperationResult<bool>.Fail("CONN_ERROR", "Error inesperado al desactivar la conexión");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<ConnectionAirlineByAirportResponseDto>>> GetConnectionsAsync(
            string codeAirportIcao, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<ConnectionAirlineByAirportResponseDto>>.FromValidation(authResult);

                var connections = await _repo.GetAirportConnectionById(codeAirportIcao);

                var responseList = connections.Select(c => new ConnectionAirlineByAirportResponseDto(
                    c.connectionId,
                    c.airportCode,
                    c.airlineCode,
                    c.airlineName,
                    c.isActive,
                    c.CreateAt
                )).ToList();

                return OperationResult<IReadOnlyCollection<ConnectionAirlineByAirportResponseDto>>.Ok(responseList);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportConnectionService.GetConnectionsAsync",
                    Message = $"Error inesperado al obtener conexiones: {codeAirportIcao}"
                }, ex);
                return OperationResult<IReadOnlyCollection<ConnectionAirlineByAirportResponseDto>>.Fail("CONN_ERROR", "Error inesperado al obtener conexiones");
            }
        }
    }
}
