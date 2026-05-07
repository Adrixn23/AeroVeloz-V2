using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Flights
{
    public class FlightBatchItemDto
    {
        [JsonPropertyName("codeAirlinesIcao")]
        public string? CodeAirlinesIcao { get; set; }

        [JsonPropertyName("originAirport")]
        public string? OriginAirport { get; set; }

        [JsonPropertyName("destinationAirport")]
        public string? DestinationAirport { get; set; }

        [JsonPropertyName("scheduledDeparture")]
        public DateTimeOffset ScheduledDeparture { get; set; }

        [JsonPropertyName("boardingGate")]
        public string? BoardingGate { get; set; }

        [JsonPropertyName("boardingGateArrived")]
        public string? BoardingGateArrived { get; set; }

        public FlightBatchItemDto() { }

        public FlightBatchItemDto(string icao, string origin, string dest, DateTimeOffset departure, string gate, string arrived)
        {
            CodeAirlinesIcao = icao;
            OriginAirport = origin;
            DestinationAirport = dest;
            ScheduledDeparture = departure;
            BoardingGate = gate;
            BoardingGateArrived = arrived;
        }
    }
}
