using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesFlightState;


namespace AeroVeloz.Domain.TransitionPolices.Polices.policyFlightCycle
{
    public class FlightLifeCyclePolicys : IFlightLifeCiclyePolicy
    {


        public bool CanTrasition(FlightStateEnum fromFlightState, FlightStateEnum toFlightState) 
        {
            if (fromFlightState == FlightStateEnum.Programado && toFlightState == FlightStateEnum.EnProceso)
            {
                return true;


            }

            if (fromFlightState == FlightStateEnum.EnProceso && toFlightState == FlightStateEnum.EnVuelo)
            {
                return true;
            }

            if (fromFlightState == FlightStateEnum.EnVuelo && toFlightState == FlightStateEnum.Desviado)
            {
                return true;
            }

            if (fromFlightState == FlightStateEnum.EnVuelo  && toFlightState == FlightStateEnum.AterrizadoArribado)
            {
                return true;
            }
           
            if (fromFlightState == FlightStateEnum.Programado && toFlightState == FlightStateEnum.Cancelado)
                 {
                    return true;
                }




            return false;

            
        }

       
    }
}
