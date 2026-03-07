using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Users.Roles
{
    public partial class Roles : BEntity<int>
    {
        public string? nameRol { get; init; }
    }
}
