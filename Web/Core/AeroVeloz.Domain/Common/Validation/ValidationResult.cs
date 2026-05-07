namespace AeroVeloz.Domain.Common.Validation
{
    /// <summary>
    /// Clase sellada que encapsula el resultado de una validación de dominio.
    /// Permite acumular errores de validación y determinar si la operación es válida.
    /// </summary>
    public sealed class ValidationResult
    {
        public ValidationResult() { } 

        public ValidationResult Success() => new();

        public ValidationResult Failur(ErrosValidationResults error)
        {
            var result = new ValidationResult();
            result._errors.Add(error);
            return result;
        }

        public ValidationResult Failur(IEnumerable<ErrosValidationResults> errores)
        {
            var result = new ValidationResult();
            result._errors.AddRange(errores);
            return result;
        }

        private readonly List<ErrosValidationResults> _errors = new();

        public bool IsValid => !_errors.Any();

        public IReadOnlyCollection<ErrosValidationResults> domainErrors => _errors;
    }
}
