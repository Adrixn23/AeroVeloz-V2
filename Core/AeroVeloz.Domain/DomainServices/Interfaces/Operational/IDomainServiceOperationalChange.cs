using AeroVeloz.Domain.Entities.Flights;

namespace AeroVeloz.Domain.Services.Interfaces.Operational
{
    public interface IDomainServiceOperationalChange
    {
        public Task ConsultStateFlightsAsync();

        public Task ManageBoardingGate();

        public Task ShowFlightPublic();

        public Task UpdateEstimatedTime();

        public bool ValidateAirportAccess(int userId, string airportCode);

        public Task ReconcileBatchInconsistency();

        public bool AuthorizeOperationalChange(int flightId,  int userId);

        public bool CanUserManagerFlight(/*User user, Flight flight*/);


  
    }
}
