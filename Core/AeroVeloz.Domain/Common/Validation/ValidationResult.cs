namespace AeroVeloz.Domain.Common.Validation
{
    public sealed class ValidationResult
    {
        public ValidationResult() { } 

        public ValidationResult Success() => new(); //
        public ValidationResult Failur(DomainError error) { // este elemento es para agregar los errores uno a uno
                var result = new ValidationResult();
                result._errors.Add(error);
                return result;
        }

        public ValidationResult Failur(IEnumerable<DomainError> errores) // y este otro para agregar un conjunto de errores
        {
            var result = new ValidationResult();
            result._errors.AddRange(errores);
            return result;
        }

        private readonly List<DomainError> _errors = new(); //list internta de los errores que seran pasados  al layered application
        public bool IsValid => !_errors.Any(); // determina si hay elementos en la list de errores
        public IReadOnlyCollection<DomainError> domainErrors => _errors; // returna la lectura completa de los errores.
       

    }
}
