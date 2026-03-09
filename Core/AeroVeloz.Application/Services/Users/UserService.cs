using System.Text.Json;
using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Events.User;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Validators.interfaces.Users;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AeroVeloz.Application.Handlers.Users
{
    public class UserService : IUserServicie
    {
        private readonly IUserRepository _repo;
        private readonly IUserValidator _validator;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IAuditRepository _auditRepo;
        private readonly IDomainServiceOrganization _orgService;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;
        private readonly IMediator _mediator;

        public UserService(
            IUserRepository repo,
            IUserValidator validator,
            IUserRepositoryAuthorization auth,
            IAuditRepository auditRepo,
            IDomainServiceOrganization orgService,
            IOrganizationMonitoringLogger monitoringLogger,
            IMediator mediator)
        {
            _repo = repo;
            _validator = validator;
            _auth = auth;
            _auditRepo = auditRepo;
            _orgService = orgService;
            _monitoringLogger = monitoringLogger;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> CreateAsync(UserSaveDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyUsers(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var hasher = new PasswordHasher<User>();
                var hash = hasher.HashPassword(null!, dto.Password!);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    nameUser = dto.UserName,
                    passwordHash = hash,
                    idOrganization = dto.IdOrganization,
                    idRol = dto.IdRol,
                    isActive = true,
                    createAt = DateTime.UtcNow,
                    failedLoginAttempts = 0
                };

                var validation = await _validator.ValidateForCreateUser(user);
                if (!validation.IsValid)
                    return OperationResult<bool>.FromValidation(validation);

                var created = await _repo.CreateEntity(user);
                if (!created)
                    return OperationResult<bool>.Fail("USER_PERSIST", "No se pudo crear el usuario");

                var org = await _orgService.GetByIdAsync(dto.IdOrganization);
                var role = await _auth.GetUserRolesAsync(user.Id, dto.IdOrganization);

                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 1,
                    idUser = userId,
                    nameEntity = "User",
                    occurentAt = DateTime.UtcNow,
                    DataNew = JsonSerializer.Serialize(new { user.Id, user.nameUser, user.idOrganization, user.idRol })
                });

                var result = OperationResult<bool>.Ok(true, "Usuario creado exitosamente");
                result.AddEvent(new UserCreatedDomainEvent(
                    user.Id, user.nameUser, user.idOrganization,
                    org?.NameOrganization, org?.TypeOrganization,
                    user.idRol, role?.nameRol, DateTime.UtcNow));

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
                    Source = "UserService.CreateAsync",
                    Message = "Error inesperado al crear usuario"
                }, ex);
                return OperationResult<bool>.Fail("USER_ERROR", "Error inesperado al crear el usuario");
            }
        }

        public async Task<OperationResult<bool>> UpdateAsync(UserUpdateDto dto, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyUsers(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var user = new User
                {
                    Id = dto.IdUser,
                    nameUser = dto.NameUser,
                    passwordHash = dto.Password,
                    isActive = dto.IsActive,
                    idRol = dto.IdRol,
                    idOrganization = dto.IdOrganization
                };

                var updated = await _repo.UpdateEntity(user);
                if (!updated)
                    return OperationResult<bool>.Fail("USER_UPDATE", "No se pudo actualizar el usuario");

                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 2,
                    idUser = userId,
                    nameEntity = "User",
                    occurentAt = DateTime.UtcNow,
                    DataNew = JsonSerializer.Serialize(new { dto.IdUser, dto.NameUser, dto.IsActive, dto.IdRol })
                });

                var result = OperationResult<bool>.Ok(true, "Usuario actualizado exitosamente");
                result.AddEvent(new UserUpdatedDomainEvent(
                    dto.IdUser, dto.NameUser, dto.IdOrganization, dto.IsActive,
                    dto.Password != null, userId, DateTime.UtcNow));

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
                    Source = "UserService.UpdateAsync",
                    Message = "Error inesperado al actualizar usuario"
                }, ex);
                return OperationResult<bool>.Fail("USER_ERROR", "Error inesperado al actualizar el usuario");
            }
        }

        public async Task<OperationResult<bool>> DeactivateAsync(Guid entityId, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanModifyUsers(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<bool>.FromValidation(authResult);

                var usersInOrg = await _repo.GetUserByOrganizationsId(orgId);
                var targetUser = usersInOrg.FirstOrDefault(u => u.idUser == entityId);
                var org = await _orgService.GetByIdAsync(orgId);

                var user = new User { Id = entityId, isActive = false };
                var deactivated = await _repo.DeleteEntity(user);
                if (!deactivated)
                    return OperationResult<bool>.Fail("USER_DEACTIVATE", "No se pudo desactivar el usuario");

                await _auditRepo.CreateAsync(new Domain.Entities.Audit.Audit
                {
                    Id = Guid.NewGuid(),
                    IdAuditType = 3,
                    idUser = userId,
                    nameEntity = "User",
                    occurentAt = DateTime.UtcNow,
                    DataOld = JsonSerializer.Serialize(new { Id = entityId, targetUser?.userName, org?.NameOrganization })
                });

                var result = OperationResult<bool>.Ok(true, "Usuario desactivado");
                result.AddEvent(new UserDeactivatedDomainEvent(
                    entityId, targetUser?.userName, orgId, org?.NameOrganization, userId, DateTime.UtcNow));

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
                    Source = "UserService.DeactivateAsync",
                    Message = $"Error inesperado al desactivar usuario: {entityId}"
                }, ex);
                return OperationResult<bool>.Fail("USER_ERROR", "Error inesperado al desactivar el usuario");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<UserDetailModel>>> GetUsersByOrganizationAsync(Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<UserDetailModel>>.FromValidation(authResult);

                var users = await _repo.GetUserByOrganizationsId(orgId);
                return OperationResult<IReadOnlyCollection<UserDetailModel>>.Ok(users);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "UserService.GetUsersByOrganizationAsync",
                    Message = "Error inesperado al obtener usuarios de la organización"
                }, ex);
                return OperationResult<IReadOnlyCollection<UserDetailModel>>.Fail("USER_ERROR", "Error inesperado al obtener usuarios");
            }
        }
    }
}
