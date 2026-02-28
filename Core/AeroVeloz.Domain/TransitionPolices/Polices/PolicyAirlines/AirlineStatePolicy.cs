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
            // Si el vuelo ya está en un estado final, no se puede tocaa
            if (flight.FlightStated == FlightStateEnum.AterrizadoArribado || flight.FlightStated == FlightStateEnum.Cancelado)
            {
                return result.Failur(ErrorAirlines.InvalidCancellationInFlight);
            }

            if (newState == FlightStateEnum.Programado && flight.FlightStated != FlightStateEnum.Programado)
            {
                return result.Failur(ErrorAirlines.InvalidRevertFlightAirline);
            }

            if (flight.FlightStated == FlightStateEnum.AterrizadoArribado || flight.FlightStated == FlightStateEnum.Cancelado)
            {

                return result.Failur(ErrorAirlines.FlightAlreadyFinalized);
            }

            if (flight.FlightStated == FlightStateEnum.EnVuelo && newState == FlightStateEnum.Cancelado)
            {
                return result.Failur(ErrorAirlines.InvalidCancellationInFlight);
            }

            if (newState == FlightStateEnum.Programado && flight.FlightStated != FlightStateEnum.Programado)
                {
                return result.Failur(ErrorAirlines.InvalidRevertFlightAirline);
                }

            return result.Success();

        }
    }
}
