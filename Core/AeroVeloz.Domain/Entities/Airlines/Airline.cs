using AeroVeloz.Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroVeloz.Domain.Entities.Organization.type;
namespace AeroVeloz.Domain.Entities.Airlines
{
    public class Airline : organization
    {
       

        public string Airlinecode { get; private set; } = null!;

        public string Name { get; private set; } = null!;
        public string CodeIATA { get; private set; } = null!;
        public int IdOrganization { get; private set; }


        public Airline(int idOrganization, TypeOrganization typeOrganization, string? emailOrganization, string airlineCode, string codeIATA, string name) : base(idOrganization, typeOrganization, emailOrganization)
        {
            this.IdOrganization = idOrganization;
                    this.Airlinecode = airlineCode;
                     this.Name = name;
            this.CodeIATA = codeIATA;
        }





    }
}
