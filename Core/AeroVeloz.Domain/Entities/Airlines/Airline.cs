using AeroVeloz.Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroVeloz.Domain.Entities.Organization.type;
using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Airlines
{
    public class Airline : organization
    {
       

        public string? Airlinecode { get; init; }

        public string? Name { get; init; }
        public string? CodeIATA { get; init; }
        public int IdOrganization { get; init; }


    }
}
