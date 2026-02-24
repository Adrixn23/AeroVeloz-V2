namespace AeroVeloz.Domain.Common
{
    public class DomainError
    {
        private string? code { get;}
        public DomainError(string? code) // constructor que recibe el codigo de error y es interpretado por application
        {
            this.code = code; 
        }
    }
}
