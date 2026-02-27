using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeError_Airlines;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline;
namespace AeroVeloz.Domain.TransitionPolices.Polices.PolicyAirlines
{

    public class AirlineStatePolicy : IAirlineStatePolicy
    {
      

        public ValidationResult EvaluateStateTransition(Flight flight, FlightStateEnum newState)
        {

            var result = new ValidationResult();

            if (flight.FlightStated == FlightStateEnum.EnVuelo && newState == FlightStateEnum.Cancelado)
            {
                return result.Failur(ErrorAirlines.InvalidCancellationInFlight);
            }

            return result.Success();

        }
    }
}
