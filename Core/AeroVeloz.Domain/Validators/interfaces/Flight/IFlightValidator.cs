using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights;

namespace AeroVeloz.Domain.Validators.interfaces.Flight
{
    public interface IFlightValidator
    {
        ValidationResult ValidateFlightRow(Entities.Flights.Flight flight);
        ValidationResult ValidateStateTransition(byte currentStateId, byte newStateId);
    }
}
