using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Domain.Models.Users
{
    public sealed record UserDetailModel(
         Guid idUser,
         string? userName,
         string? nameUser,
         string? nameOrganization,
         string? OrganizationType,
         bool isActive,
         bool isBlocked,
         string? nameRol,
         DateTime createAt
        );

}
