namespace AeroVeloz.Domain.TransitionPolices
{
    public interface IFlightLifeCiclyePolicy
    {
        //descomentar cuando se cree el enum correspondiente
        public bool CanTrasition(/*FlightState from, FlightSate to*/);
    }
}
