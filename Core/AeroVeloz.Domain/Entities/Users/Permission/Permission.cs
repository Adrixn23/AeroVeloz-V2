using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users.Permission
{
    public partial class Permission : BEntity<int>
    {
        public string? codePermision { get; init; }
        public string? description { get; init; }
    }
}
