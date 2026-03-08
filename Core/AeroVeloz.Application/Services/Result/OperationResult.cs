using AeroVeloz.Domain.Common.Validation;
using MediatR;

namespace AeroVeloz.Application.Services.Result
{
  
    public sealed class OperationResult<T>
    {
        public T? Value { get; private init; }

        public bool Success { get; private init; }

        public string? Message { get; private init; }

        public string? ErrorCode { get; private init; }

        private readonly List<INotification> _domainEvents = [];

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        private readonly List<ErrosValidationResults> _validationErrors = [];

        public IReadOnlyCollection<ErrosValidationResults> ValidationErrors => _validationErrors.AsReadOnly();

        public bool HasValidationErrors => _validationErrors.Count > 0;

        public bool HasDomainEvents => _domainEvents.Count > 0;

        private OperationResult() { }
      
        public static OperationResult<T> Ok(T value, string? message = null)
            => new() { Value = value, Success = true, Message = message };

     
        public static OperationResult<T> Fail(string errorCode, string message)
            => new() { Success = false, ErrorCode = errorCode, Message = message };

   
        public static OperationResult<T> FromValidation(ValidationResult validationResult)
        {
            var result = new OperationResult<T> { Success = false, Message = "Errores de validación del dominio" };
            result._validationErrors.AddRange(validationResult.domainErrors);
            return result;
        }

      
        public static OperationResult<T> Fail(ErrosValidationResults error)
        {
            var result = new OperationResult<T>
            {
                Success = false,
                ErrorCode = error.code,
                Message = error.description
            };
            result._validationErrors.Add(error);
            return result;
        }

        
        public void AddEvent(INotification @event) => _domainEvents.Add(@event);

        public void AddEvents(IEnumerable<INotification> events) => _domainEvents.AddRange(events);
    }
}
