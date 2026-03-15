using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.DTOs.Auth;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Events.User;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.Handlers.Auth
{
    public class AuthenticationService : IAuthenticationServicie
    {
        private readonly IUserRepositoryAuthenticacion _authRepo;
        private readonly IUserRepository _userRepo;
        private readonly IUserRepositoryAuthorization _authzRepo;
        private readonly IDomainServiceOrganization _orgService;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;
        private readonly IMediator _mediator;

        public AuthenticationService(
            IUserRepositoryAuthenticacion authRepo,
            IUserRepository userRepo,
            IUserRepositoryAuthorization authzRepo,
            IDomainServiceOrganization orgService,
            IOrganizationMonitoringLogger monitoringLogger,
            IMediator mediator)
        {
            _authRepo = authRepo;
            _userRepo = userRepo;
            _authzRepo = authzRepo;
            _orgService = orgService;
            _monitoringLogger = monitoringLogger;
            _mediator = mediator;
        }

        public async Task<OperationResult<UserLoginResultDto>> LoginAsync(UserLoginDto dto)
        {
            try
            {
                var org = await _orgService.GetByEmailAsync(dto.emailOrganization!);
                if (org == null)
                    return OperationResult<UserLoginResultDto>.Fail("LOGIN_ORG", "Organización no encontrada");


                var orgAccess = await _authRepo.IsOrganizationAccessAllowedAsync(org.Id);
                if (!orgAccess.IsValid)
                    return OperationResult<UserLoginResultDto>.FromValidation(orgAccess);


                var userSystem = await _userRepo.GetByUserName(dto.nameUser!, org.Id);
                if (userSystem == null)
                    return OperationResult<UserLoginResultDto>.Fail("LOGIN_USER", "Usuario no encontrado en esta organización");


                if (userSystem.lockedUntil.HasValue && userSystem.lockedUntil.Value > DateTime.UtcNow)
                {
                    await _monitoringLogger.LogSecurityAlertAsync(new MonitoringLogEntry
                    {
                        OrganizationId = org.Id,
                        UserId = userSystem.userId,
                        Source = "AuthenticationServie",
                        Message = $"Intento de acceso a cuenta bloqueada: {dto.nameUser}"
                    });
                    return OperationResult<UserLoginResultDto>.Fail(AuthenticationErrors.AccountLockedByAttempts);
                }

                var credentials = await _authRepo.ValidateUserCredentialsAsync(dto.nameUser!, dto.password!, org.Id);
                if (!credentials.IsValid)
                {
                 var newAttempts = userSystem.failedLoginAttempts + 1;
                    DateTime? lockUntil = null;

                    if (newAttempts >= 3)
                        lockUntil = DateTime.UtcNow.AddMinutes(15);

                    await _authRepo.RegisterLoginAttemptAsync(
                        userSystem.userId, newAttempts, lockUntil ?? DateTime.UtcNow, org.Id);

                    await _mediator.Publish(new UserLoginFailedDomainEvent(
                        userSystem.userId, dto.nameUser, org.Id, org.NameOrganization,
                        newAttempts, DateTime.UtcNow));

                    if (newAttempts >= 3)
                    {
                        await _mediator.Publish(new UserAccountLockedDomainEvent(
                            userSystem.userId, dto.nameUser, org.Id, org.NameOrganization,
                            lockUntil!.Value, newAttempts, DateTime.UtcNow));

                        await _monitoringLogger.LogSecurityAlertAsync(new MonitoringLogEntry
                        {
                            OrganizationId = org.Id,
                            UserId = userSystem.userId,
                            Source = "AuthenticationServie",
                            Message = $"Cuenta bloqueada por {newAttempts} intentos fallidos: {dto.nameUser}"
                        });

                        return OperationResult<UserLoginResultDto>.Fail(AuthenticationErrors.AccountLockedByAttempts);
                    }

                    return OperationResult<UserLoginResultDto>.FromValidation(credentials);
                }

                var isActive = await _authRepo.IsUserActiveAsync(userSystem.userId, org.Id);

                if (!isActive.IsValid)
                    return OperationResult<UserLoginResultDto>.FromValidation(isActive);

                var role = await _authzRepo.GetUserRolesAsync(userSystem.userId, org.Id);

                var allowedRoles = new[] { "AIRPORTADMIN", "SYSTEMADMIN", "OPERATIONAIRPORT" };

                if (!allowedRoles.Any(r => string.Equals(role.nameRol, r, StringComparison.OrdinalIgnoreCase)))
                {
                    await _monitoringLogger.LogSecurityAlertAsync(new MonitoringLogEntry
                    {
                        OrganizationId = org.Id,
                        UserId = userSystem.userId,
                        Source = "AuthenticationService",
                        Message = $"Acceso denegado al portal de escritorio para rol '{role.nameRol}': {dto.nameUser}"
                    });
                    return OperationResult<UserLoginResultDto>.Fail(AuthenticationErrors.DesktopAccessDenied);
                }
                /////

                await _authRepo.RegisterLoginAttemptAsync(
                    userSystem.userId, 0, DateTime.UtcNow, org.Id);

                var loginResult = new UserLoginResultDto(
                    userSystem.userId,
                    userSystem.nameUser,
                    org.Id,
                    org.NameOrganization,
                    org.TypeOrganization,
                    role.nameRol
                );

                return OperationResult<UserLoginResultDto>.Ok(loginResult, "Inicio de sesión exitoso");
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = "AuthenticationService.LoginAsync",
                    Message = "Error inesperado durante el inicio de sesión"
                }, ex);
                return OperationResult<UserLoginResultDto>.Fail("LOGIN_ERROR", "Error inesperado durante el inicio de sesión");
            }
        }
    }
}
