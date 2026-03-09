using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Events.Aiport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Validators.interfaces.Airport;
using MediatR;

namespace AeroVeloz.Application.Handlers.Airport
{
    public class AirportConnectionService : IAirportConnectionServicie
    {
        private readonly IAirportConnectionAirline _repo;
        private readonly IConnectionAiportAirline _validator;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IMediator _mediator;

        public AirportConnectionService(
            IAirportConnectionAirline repo,
            IConnectionAiportAirline validator,
            IUserRepositoryAuthorization auth,
            IMediator mediator)
        {
            _repo = repo;
            _validator = validator;
            _auth = auth;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> CreateConnectionAsync(ConnectionAirlineByAirportSaveDto dto, Guid userId, int orgId)
        {
            var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<bool>.FromValidation(authResult);

            var connection = new ConectionsAirlineAirport
            {
                Id = Guid.NewGuid(),
                codeAirlines = dto.codeAirline,
                codeAirport = dto.codeAirport,
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
                connection.Id, dto.codeAirport, dto.codeAirline, userId, DateTime.UtcNow));

            foreach (var evt in result.DomainEvents)
                await _mediator.Publish(evt);

            return result;
        }

        public async Task<OperationResult<bool>> DeactivateConnectionAsync(Guid connectionId, Guid userId, int orgId)
        {
            var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<bool>.FromValidation(authResult);

            var connection = new ConectionsAirlineAirport { Id = connectionId, isActive = false };
            var deactivated = await _repo.DeleteEntity(connection);
            if (!deactivated)
                return OperationResult<bool>.Fail("CONN_DEACTIVATE", "No se pudo desactivar la conexión");

            var result = OperationResult<bool>.Ok(true, "Conexión desactivada");
            result.AddEvent(new AirportConnectionDeactivatedDomainEvent(
                connectionId, null, null, userId, DateTime.UtcNow));

            foreach (var evt in result.DomainEvents)
                await _mediator.Publish(evt);

            return result;
        }

        public async Task<OperationResult<IReadOnlyCollection<AirlineConnectionByAirportModel>>> GetConnectionsAsync(
            string codeAirportIcao, Guid userId, int orgId)
        {
            var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<IReadOnlyCollection<AirlineConnectionByAirportModel>>.FromValidation(authResult);

            var connections = await _repo.GetAirportConnectionById(codeAirportIcao);
            return OperationResult<IReadOnlyCollection<AirlineConnectionByAirportModel>>.Ok(connections);
        }
    }
}
