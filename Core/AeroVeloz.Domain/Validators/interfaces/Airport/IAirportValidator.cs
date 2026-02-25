using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Organization.Airports;

namespace AeroVeloz.Domain.Validators.interfaces.Airports
{
    public interface IAirportValidator
    {
        ValidationResult Validation(Airport airport);
    }
}
