using System.Text.Json;
using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Events.Aiport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Validators.interfaces.Airports;
using MediatR;

namespace AeroVeloz.Application.Handlers.Airport
{
    public class AirportServicie : IAirportServicie
    {
        private readonly IAirportRepository _repo;
        private readonly IAirportValidator _validator;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IAuditRepository _auditRepo;
        private readonly IMediator _mediator;

        public AirportServicie(
            IAirportRepository repo,
            IAirportValidator validator,
            IUserRepositoryAuthorization auth,
            IAuditRepository auditRepo,
            IMediator mediator)
        {
            _repo = repo;
            _validator = validator;
            _auth = auth;
            _auditRepo = auditRepo;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> CreateAsync(AirportSaveDto dto, Guid userId, int orgId)
        {
            var authResult = await _auth.CanModifyOrganizations(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<bool>.FromValidation(authResult);

            var airport = new Domain.Entities.Organization.Airports.Airport
            {
                codeAirportIcao = dto.codeICAO,
                codeAirportIata = dto.codeIATA,
                country = dto.country,
                city = dto.city,
                timeOffset = dto.timeOffset,
                nameOrganization = dto.nameOrganization,
                typeOrganization = dto.typeOrganization ?? "AIRPORT",
                emailOrganization = dto.emailOrganization,
                isActived = true,
                createAt = DateTime.UtcNow
            };

            var validation = await _validator.ValidateForCreateAirport(airport);
            if (!validation.IsValid)
                return OperationResult<bool>.FromValidation(validation);

            var created = await _repo.CreateEntity(airport);
            if (!created)
                return OperationResult<bool>.Fail("AIRPORT_PERSIST", "No se pudo registrar el aeropuerto");

            await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
            {
                Id = Guid.NewGuid(),
                IdAuditType = 1,
                idUser = userId,
                nameEntity = "Airport",
                occurentAt = DateTime.UtcNow,
                DataNew = JsonSerializer.Serialize(dto)
            });

            var result = OperationResult<bool>.Ok(true, "Aeropuerto registrado exitosamente");
            result.AddEvent(new AirportRegisteredDomainEvent(
                dto.codeICAO, dto.codeIATA, dto.nameOrganization,
                dto.country, dto.city, dto.emailOrganization, DateTime.UtcNow));

            foreach (var evt in result.DomainEvents)
                await _mediator.Publish(evt);

            return result;
        }

        public async Task<OperationResult<bool>> UpdateAsync(AirportUpdateDto dto, Guid userId, int orgId)
        {
            var authResult = await _auth.CanModifyOrganizations(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<bool>.FromValidation(authResult);

            var oldAirport = await _repo.GetAirportByCode(dto.codeICAO);

            var airport = new Domain.Entities.Organization.Airports.Airport
            {
                Id = orgId,
                codeAirportIcao = dto.codeICAO,
                codeAirportIata = dto.codeIATA,
                country = dto.country,
                city = dto.city,
                timeOffset = dto.timeOffset,
                nameOrganization = dto.nameOrganization,
                typeOrganization = dto.typeOrganization ?? "AIRPORT",
                emailOrganization = dto.emailOrganization,
                isActived = true,
                createAt = DateTime.UtcNow
            };

            var updated = await _repo.UpdateEntity(airport);
            if (!updated)
                return OperationResult<bool>.Fail("AIRPORT_UPDATE", "No se pudo actualizar el aeropuerto");

            await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit 
            {
                Id = Guid.NewGuid(),
                IdAuditType = 2,
                idUser = userId,
                nameEntity = "Airport",
                occurentAt = DateTime.UtcNow,
                DataOld = JsonSerializer.Serialize(oldAirport),
                DataNew = JsonSerializer.Serialize(dto)
            });

            var result = OperationResult<bool>.Ok(true, "Aeropuerto actualizado exitosamente");
            result.AddEvent(new AirportUpdatedDomainEvent(
                dto.codeICAO, dto.codeIATA, dto.nameOrganization,
                dto.country, dto.city, userId, DateTime.UtcNow));

            foreach (var evt in result.DomainEvents)
                await _mediator.Publish(evt);

            return result;
        }

        public async Task<OperationResult<bool>> DeactivateAsync(int entityId, Guid userId, int orgId)
        {
            var authResult = await _auth.CanModifyOrganizations(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<bool>.FromValidation(authResult);

            var airport = new Domain.Entities.Organization.Airports.Airport { Id = entityId, isActived = false };
            var deactivated = await _repo.DeleteEntity(airport);
            if (!deactivated)
                return OperationResult<bool>.Fail("AIRPORT_DEACTIVATE", "No se pudo desactivar el aeropuerto");

            await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
            {
                Id = Guid.NewGuid(),
                IdAuditType = 3,
                idUser = userId,
                nameEntity = "Airport",
                occurentAt = DateTime.UtcNow,
                DataOld = JsonSerializer.Serialize(new { Id = entityId })
            });

            var result = OperationResult<bool>.Ok(true, "Aeropuerto desactivado");
            result.AddEvent(new AirportSuspendedDomainEvent(null, null, null, userId, DateTime.UtcNow));

            foreach (var evt in result.DomainEvents)
                await _mediator.Publish(evt);

            return result;
        }

        public async Task<OperationResult<IReadOnlyCollection<AirportModel>>> GetAllAsync(Guid userId, int orgId)
        {
            var authResult = await _auth.CanModifyOrganizations(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<IReadOnlyCollection<AirportModel>>.FromValidation(authResult);

            var airports = await _repo.GetAllAirport();
            return OperationResult<IReadOnlyCollection<AirportModel>>.Ok(airports);
        }

        public async Task<OperationResult<AirportModel>> GetByCodeAsync(string codeAirport, Guid userId, int orgId)
        {
            var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<AirportModel>.FromValidation(authResult);

            var airport = await _repo.GetAirportByCode(codeAirport);
            return OperationResult<AirportModel>.Ok(airport);
        }

        public async Task<OperationResult<bool>> GenerateApiKeyAsync(string codeAirport, Guid userId, int orgId)
        {
            var authResult = await _auth.CanModifyOrganizations(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<bool>.FromValidation(authResult);

            var generated = await _repo.GenerateApiKey(codeAirport);
            if (!generated)
                return OperationResult<bool>.Fail("AIRPORT_APIKEY", "No se pudo generar la API Key");

            var result = OperationResult<bool>.Ok(true, "API Key generada exitosamente");
            result.AddEvent(new AirportApiKeyGeneratedDomainEvent(codeAirport, null, null, userId, DateTime.UtcNow));

            foreach (var evt in result.DomainEvents)
                await _mediator.Publish(evt);

            return result;
        }
    }
}
