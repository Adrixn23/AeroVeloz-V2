using AeroVeloz.Domain.Entities.Organization.Base;
namespace AeroVeloz.Domain.Entities.Organization.Airports
{

    public partial class Airport : Organizations
    {
        public string? codeAirportIcao { get; init; }
        public string? codeAirportIata { get; init; }
        public string? country { get; init; }
        public string? city { get; init; }
        public string? apiKeyMaster {  get; init; }
       public DateTimeOffset timeOffset { get; init; }
  
    }


}


