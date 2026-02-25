namespace AeroVeloz.Domain.Common
{
    public  class ValidationResult
    {
      
    
        private readonly List<DomainError> _errors = new(); //list internta de los errores que seran pasados  al layered application
        public bool IsValid => !_errors.Any(); // determina si hay elementos en la list de errores
        public IReadOnlyCollection<DomainError> domainErrors => _errors; // returna la lectura completa de los errores.
        public void AddError(DomainError error)
        {
            _errors.Add(error);
        }

    }
}
