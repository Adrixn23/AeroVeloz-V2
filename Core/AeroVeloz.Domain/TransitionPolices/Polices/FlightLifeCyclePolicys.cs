using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.TransitionPolices;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesFlightState;


namespace AeroVeloz.Domain.Flights
{
    public class FlightLifeCyclePolicys : IFlightLifeCiclyePolicy
    {

        //cuando se agreguen los elementos descriptos en la interfaces modificar aqui 
       
        

        public bool CanTrasition(FlightStateEnum fromFlightState, FlightStateEnum toFlightState)
        //agregar la implementacion y logica de negocio  del metodo 
        {
            throw new NotImplementedException();
        }
    }
}
