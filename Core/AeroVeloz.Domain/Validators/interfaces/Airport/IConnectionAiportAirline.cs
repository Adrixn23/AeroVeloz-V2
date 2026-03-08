using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Organization.Airport;

namespace AeroVeloz.Domain.Validators.interfaces.Airport
{
    public interface IConnectionAiportAirline
    {
        Task<ValidationResult> ValidationForCreateConnectionAirlineByAirport(ConectionsAirlineAirport contections);
    }
}
