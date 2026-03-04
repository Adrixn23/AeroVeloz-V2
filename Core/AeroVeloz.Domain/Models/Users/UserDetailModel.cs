using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Domain.Models.Users
{
    public class UserDetailModel
    {
        public Guid idUser { get; }
        public string? userName { get; }
        public OrganizationType OrganizationType { get; }

    }
}
