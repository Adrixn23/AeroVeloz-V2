using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Organization.Airport
{
    public class ContectionsAirlineAirport : BEntity<Guid>
    {
        public string? codeAirlines { get; private set; }
        public string? codeAirport { get; private set;  }
        public string? tokenApi { get; private set; }

        private ContectionsAirlineAirport(Guid id, string? codeAirlines, string? codeAirport,  string? tokenApi)
        {
            Id = id;
            this.codeAirlines = codeAirlines;
            this.codeAirport  = codeAirport;
            this.tokenApi = tokenApi;
        }

    }
}
