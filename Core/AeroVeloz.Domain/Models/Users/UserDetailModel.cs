using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Domain.Models.Users
{
    public sealed record UserDetailModel(
         Guid idUser,
         string? userName,
         string? nameOrganization,
         OrganizationType OrganizationType,
         bool isActive,
         List<Roles> roles,
         List<Permission> Permissions,
         DateTime createAt
        );
   
}
