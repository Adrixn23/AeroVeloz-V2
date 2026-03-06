using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Organization.type
{
    public sealed class TypeOrganization : BEntity<int>
    {
        public static readonly TypeOrganization Airport = new(1, "Airport");

        public static readonly TypeOrganization Airline = new(2, "Airline");

        public string? name { get; private set;}   
        public TypeOrganization(int typeOrganization, string organizationName)
        {
            Id = typeOrganization;
            name = organizationName;
        }
    }
}
