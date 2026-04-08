namespace AeroVeloz.Web.Models.Flights
{
    public class FlightUpdateStateDto
    {
        public short FlightNumber { get; set; }
        public string CodeAirlinesIcao { get; set; } = string.Empty;
        public byte FlightStateId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
