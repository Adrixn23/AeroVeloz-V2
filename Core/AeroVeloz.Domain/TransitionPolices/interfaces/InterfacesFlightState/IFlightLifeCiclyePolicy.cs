using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesFlightState;

    public interface IFlightLifeCiclyePolicy
    {
    //descomentar cuando se cree el enum correspondiente
    public bool CanTrasition(FlightStateEnum fromFlightState, FlightStateEnum toFlightState);
    
}


