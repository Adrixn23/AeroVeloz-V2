namespace AeroVeloz.Domain.TransitionPolices
{
    public interface IVisibilityPolicy
    {

        //descomentar cuando se creen los dto de la capa de application

         bool CanSeeField(string role, string fieldName);
        //public FlightDto ApplyVisibility (Flight flight, role  string);

         bool IsVisibleToPublic(DateTime flightDate, DateTime now);
    }
}
