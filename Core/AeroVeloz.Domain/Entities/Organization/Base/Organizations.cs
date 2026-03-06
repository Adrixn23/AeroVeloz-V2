using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Organization.Base
{
    public abstract class Organizations : BEntity<int>
    {
        public string? nameOrganization { get; init; }
        public  TypeOrganization typeOrganization  { get; init; }
        public bool isActived { get; init; }
        public string? emailOrganization { get; init; }
        public DateTime createAt { get; init; }
      
    }
}
