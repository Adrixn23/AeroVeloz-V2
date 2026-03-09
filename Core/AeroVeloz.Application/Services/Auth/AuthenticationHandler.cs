using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.DTOs.Auth;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Services.Result;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.DomainService.Interfaces.Organization;

namespace AeroVeloz.Application.Services.Auth
{
    public class AuthenticationHandler : IAuthenticationServicie
    {
        private readonly IUserRepositoryAuthenticacion _authRepo;
        private readonly IUserRepositoryAuthorization _authzRepo;
        private readonly IDomainServiceOrganization _orgService;

        public AuthenticationHandler(
            IUserRepositoryAuthenticacion authRepo,
            IUserRepositoryAuthorization authzRepo,
            IDomainServiceOrganization orgService)
        {
            _authRepo = authRepo;
            _authzRepo = authzRepo;
            _orgService = orgService;
        }

        public async Task<OperationResult<UserLoginResultDto>> LoginAsync(UserLoginDto dto)
        {
            var org = await _orgService.GetByEmailAsync(dto.emailOrganization!);
            if (org == null)
                return OperationResult<UserLoginResultDto>.Fail("LOGIN_ORG", "Organización no encontrada");

            var orgAccess = await _authRepo.IsOrganizationAccessAllowedAsync(org.Id);
            if (!orgAccess.IsValid)
                return OperationResult<UserLoginResultDto>.FromValidation(orgAccess);

            var credentials = await _authRepo.ValidateUserCredentialsAsync(dto.nameUser!, dto.password!, org.Id);
            if (!credentials.IsValid)
                return OperationResult<UserLoginResultDto>.FromValidation(credentials);

            var userSystem = await _authRepo.GetByUserNameAsync(dto.nameUser!, org.Id);

            var isActive = await _authRepo.IsUserActiveAsync(userSystem.userId, org.Id);
            if (!isActive.IsValid)
                return OperationResult<UserLoginResultDto>.FromValidation(isActive);

            var isLocked = await _authRepo.IsUserLockedAsync(userSystem.userId, org.Id);
            if (!isLocked.IsValid)
                return OperationResult<UserLoginResultDto>.FromValidation(isLocked);

            var role = await _authzRepo.GetUserRolesAsync(userSystem.userId, org.Id);

            if (role.nameRol != "AIRLINEADMIN")
                return OperationResult<UserLoginResultDto>.Fail("LOGIN_ROLE", "Este aplicativo es exclusivo para administradores de aerolínea");

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
    }
}
