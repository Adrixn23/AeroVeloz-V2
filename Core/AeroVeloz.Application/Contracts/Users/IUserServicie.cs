using AeroVeloz.Application.Contracts.Base;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Users;

namespace AeroVeloz.Application.Contracts.Users
{
    public interface IUserServicie : IBaseServicie<UserSaveDto, UserUpdateDto, Guid>
    {
        Task<OperationResult<IReadOnlyCollection<UserDetailModel>>> GetUsersByOrganizationAsync(Guid userId, int orgId);
    }
}
