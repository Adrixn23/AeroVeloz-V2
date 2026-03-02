using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User
{
    public interface IDomainServiceUser
    {
       Task<ValidationResult> Validate
    
    }
}
