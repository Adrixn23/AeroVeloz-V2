using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.DTOs.Auth;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;

namespace AeroVeloz.Application.Handlers.Auth
{
    public class AuthenticationHandler : IAuthenticationServicie
    {
        private readonly IUserRepositoryAuthenticacion _authRepo;
        private readonly IUserRepository _userRepo;
        private readonly IUserRepositoryAuthorization _authzRepo;
        private readonly IDomainServiceOrganization _orgService;

        public AuthenticationHandler(
            IUserRepositoryAuthenticacion authRepo,
            IUserRepository userRepo,
            IUserRepositoryAuthorization authzRepo,
            IDomainServiceOrganization orgService)
        {
            _authRepo = authRepo;
            _userRepo = userRepo;
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

            var userSystem = await _userRepo.GetByUserName(dto.nameUser!, org.Id);

            var isActive = await _authRepo.IsUserActiveAsync(userSystem.userId, org.Id);
            if (!isActive.IsValid)
                return OperationResult<UserLoginResultDto>.FromValidation(isActive);

            var isLocked = await _authRepo.IsUserLockedAsync(userSystem.userId, org.Id);
            if (!isLocked.IsValid)
                return OperationResult<UserLoginResultDto>.FromValidation(isLocked);

            var role = await _authzRepo.GetUserRolesAsync(userSystem.userId, org.Id);

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
