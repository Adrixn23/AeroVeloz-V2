using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Flights
{
    public class FlightReadDto
    {
        [JsonPropertyName("flightNumber")]
        public short FlightNumber { get; set; }

        [JsonPropertyName("codeAirlinesIcao")]
        public string CodeAirlinesIcao { get; set; } = string.Empty;

        [JsonPropertyName("originAirport")]
        public string OriginAirport { get; set; } = string.Empty;

        [JsonPropertyName("destinationAirport")]
        public string DestinationAirport { get; set; } = string.Empty;

        [JsonPropertyName("scheduledDeparture")]
        public DateTimeOffset ScheduledDeparture { get; set; }

        [JsonPropertyName("boardingGate")]
        public string BoardingGate { get; set; } = string.Empty;

        [JsonPropertyName("boardingGateArrived")]
        public string BoardingGateArrived { get; set; } = string.Empty;

        [JsonPropertyName("flightStateId")]
        public byte FlightStateId { get; set; }

        [JsonPropertyName("flightStateName")]
        public string FlightStateName { get; set; } = string.Empty;
    }
}
