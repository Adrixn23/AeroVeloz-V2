using AeroVeloz.Domain.Entities.Organization.type;

namespace AeroVeloz.Domain.Entities.BaseEntity
{
    public abstract class organization : BEntity<int>
    {
        public TypeOrganization? typeOrganization { get; private set; }
        public bool isActived { get; private set; }
        public string? emailOrganization { get; private set; }
        public DateTime createAt { get; private set; }
        
}

}