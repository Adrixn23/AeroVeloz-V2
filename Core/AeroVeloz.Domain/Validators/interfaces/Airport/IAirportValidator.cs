using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Organization.Airports;

namespace AeroVeloz.Domain.Validators.interfaces.Airports
{
    public interface IAirportValidator
    {
        Task<ValidationResult> ValidateForCreateAirport(Airport airport);
    }


}