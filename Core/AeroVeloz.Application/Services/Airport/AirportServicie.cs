using System.Security.Cryptography;
using System.Text.Json;
using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Common.CodeErrors;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Events.Aiport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Validators.interfaces.Airports;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AeroVeloz.Application.Handlers.Airport
{
    public class AirportServicie : IAirportServicie
    {
        private readonly IAirportRepository _repo;
        private readonly IAirportValidator _validator;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IAuditRepository _auditRepo;
        private readonly IUserRepository _userRepo;
        private readonly IDomainServiceOrganization _orgService;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;
        private readonly IMediator _mediator;

        public AirportServicie(
            IAirportRepository repo,
            IAirportValidator validator,
            IUserRepositoryAuthorization auth,
            IAuditRepository auditRepo,
            IUserRepository userRepo,
            IDomainServiceOrganization orgService,
            IOrganizationMonitoringLogger monitoringLogger,
            IMediator mediator)
        {
            _repo = repo;
            _validator = validator;
            _auth = auth;
            _auditRepo = auditRepo;
            _userRepo = userRepo;
            _orgService = orgService;
            _monitoringLogger = monitoringLogger;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> CreateAsync(AirportSaveDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOrganizations(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var orgExits = await _orgService.GetByEmailAsync(dto.emailOrganization!);

                if(orgExits != null)
                {
                    var vl = new ValidationResult().Failur(AirportErrors.AirportExistInSystem);
                    return OperationResult<bool>.FromValidation(vl);
                }

                var airport = new Domain.Entities.Organization.Airports.Airport
                {
                    codeAirportIcao = dto.codeICAO,
                    codeAirportIata = dto.codeIATA,
                    country = dto.country,
                    city = dto.city,
                    timeOffset = dto.timeOffset,
                    nameOrganization = dto.nameOrganization,
                    typeOrganization = "AIRPORT",
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

                var rawPassword = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
                var hasher = new PasswordHasher<Domain.Entities.Users.User.User>();
                var hash = hasher.HashPassword(null!, rawPassword);
                var defaultUserName = $"admin_{dto.codeICAO}";

                var org = await _orgService.GetByEmailAsync(dto.emailOrganization!);

                Domain.Entities.Users.User.User user = null!;
                if (org != null)
                {
                    var defaultUser = new Domain.Entities.Users.User.User
                    {
                        Id = Guid.NewGuid(),
                        nameUser = defaultUserName,
                        passwordHash = hash,
                        idOrganization = org.Id,
                        idRol = 2,
                        isActive = true,
                        createAt = DateTime.UtcNow,
                        failedLoginAttempts = 0
                    };
                    user = defaultUser;
                    await _userRepo.CreateEntity(defaultUser);
                }

                var values = JsonSerializer.Serialize(airport);
                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 1,
                    idUser = userId,
                    nameEntity = "Airport",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = values
                });

                var valuesUserByAirport = JsonSerializer.Serialize(user);
                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 1,
                    idUser = userId,
                    nameEntity = "Users",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = valuesUserByAirport
                });

                var result = OperationResult<bool>.Ok(true, "Aeropuerto registrado exitosamente");

                result.AddEvent(new AirportRegisteredDomainEvent(
                    dto.codeICAO, dto.codeIATA, dto.nameOrganization,
                    dto.country, dto.city, dto.emailOrganization,
                    defaultUserName, rawPassword, DateTime.UtcNow));

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
                    Source = "AirportServicie.CreateAsync",
                    Message = "Error de base de datos al registrar aeropuerto"
                }, ex);
                return OperationResult<bool>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportServicie.CreateAsync",
                    Message = "Error inesperado al registrar aeropuerto"
                }, ex);
                return OperationResult<bool>.Fail("AIRPORT_ERROR", "Error inesperado al registrar el aeropuerto");
            }
        }

        public async Task<OperationResult<bool>> UpdateAsync(AirportUpdateDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOrganizations(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var oldAirport = await _repo.GetAirportByCode(dto.codeICAO);

                var airport = new Domain.Entities.Organization.Airports.Airport
                {
                    Id = dto.idOrg,
                    codeAirportIcao = dto.codeICAO,
                    codeAirportIata = dto.codeIATA,
                    country = dto.country,
                    city = dto.city,
                    timeOffset = dto.timeOffset,
                    nameOrganization = dto.nameOrganization,
                    typeOrganization = "AIRPORT",
                    emailOrganization = dto.emailOrganization,
                    isActived = true,
                    createAt = DateTime.UtcNow
                };

                var updated = await _repo.UpdateEntity(airport);
                if (!updated)
                    return OperationResult<bool>.Fail("AIRPORT_UPDATE", "No se pudo actualizar el aeropuerto");


                var values = JsonSerializer.Serialize(airport);
                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 2,
                    idUser = userId,
                    nameEntity = "Airport",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = values
                });

                var result = OperationResult<bool>.Ok(true, "Aeropuerto actualizado exitosamente");
                result.AddEvent(new AirportUpdatedDomainEvent(
                    dto.codeICAO, dto.codeIATA, dto.nameOrganization,
                    dto.country, dto.city, userId, DateTime.UtcNow));

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
                    Source = "AirportServicie.UpdateAsync",
                    Message = "Error de base de datos al actualizar aeropuerto"
                }, ex);
                return OperationResult<bool>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportServicie.UpdateAsync",
                    Message = "Error inesperado al actualizar aeropuerto"
                }, ex);
                return OperationResult<bool>.Fail("AIRPORT_ERROR", "Error inesperado al actualizar el aeropuerto");
            }
        }

        public async Task<OperationResult<bool>> DeactivateAsync(int entityId, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOrganizations(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var orgData = await _orgService.GetByIdAsync(entityId);

                var airport = new Domain.Entities.Organization.Airports.Airport { Id = entityId, isActived = false };
                var deactivated = await _repo.DeleteEntity(airport);

                if (!deactivated)
                    return OperationResult<bool>.Fail("AIRPORT_DEACTIVATE", "No se pudo desactivar el aeropuerto");

                var values = JsonSerializer.Serialize(airport);
                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 3,
                    idUser = userId,
                    nameEntity = "Airport",
                    ocurrentAt = DateTime.UtcNow,
                    newValuesData = values
                });

                var result = OperationResult<bool>.Ok(true, "Aeropuerto desactivado");


                result.AddEvent(new AirportSuspendedDomainEvent(
                     orgData?.NameOrganization, userId, DateTime.UtcNow));

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
                    Source = "AirportServicie.DeactivateAsync",
                    Message = "Error de base de datos al desactivar aeropuerto"
                }, ex);
                return OperationResult<bool>.Fail(SystemErrors.DatabaseFailure);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportServicie.DeactivateAsync",
                    Message = "Error inesperado al desactivar aeropuerto"
                }, ex);
                return OperationResult<bool>.Fail("AIRPORT_ERROR", "Error inesperado al desactivar el aeropuerto");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<AirportModel>>> GetAllAsync(Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOrganizations(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<AirportModel>>.FromValidation(authResult);

                var airports = await _repo.GetAllAirport();
                return OperationResult<IReadOnlyCollection<AirportModel>>.Ok(airports);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportServicie.GetAllAsync",
                    Message = "Error inesperado al obtener aeropuertos"
                }, ex);
                return OperationResult<IReadOnlyCollection<AirportModel>>.Fail("AIRPORT_ERROR", "Error inesperado al obtener aeropuertos");
            }
        }

        public async Task<OperationResult<AirportModel>> GetByCodeAsync(string codeAirport, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<AirportModel>.FromValidation(authResult);

                var airport = await _repo.GetAirportByCode(codeAirport);
                return OperationResult<AirportModel>.Ok(airport);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AirportServicie.GetByCodeAsync",
                    Message = $"Error inesperado al obtener aeropuerto por código: {codeAirport}"
                }, ex);
                return OperationResult<AirportModel>.Fail("AIRPORT_ERROR", "Error inesperado al obtener el aeropuerto");
            }
        }

        public async Task<OperationResult<bool>> GenerateApiKeyAsync(string codeAirport, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyOrganizations(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var airportData = await _repo.GetAirportByCode(codeAirport);

                var generated = await _repo.GenerateApiKey(codeAirport);
                if (!generated)
                    return OperationResult<bool>.Fail("AIRPORT_APIKEY", "No se pudo generar la API Key");

                var result = OperationResult<bool>.Ok(true, "API Key generada exitosamente");
                result.AddEvent(new AirportApiKeyGeneratedDomainEvent(
                    codeAirport, airportData?.codeAirportIata, airportData?.nameAirport, userId, DateTime.UtcNow));

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
                    Source = "AirportServicie.GenerateApiKeyAsync",
                    Message = $"Error inesperado al generar API Key: {codeAirport}"
                }, ex);
                return OperationResult<bool>.Fail("AIRPORT_ERROR", "Error inesperado al generar la API Key");
            }
        }
    }
}
