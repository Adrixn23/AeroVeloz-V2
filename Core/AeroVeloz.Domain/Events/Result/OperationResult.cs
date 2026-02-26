namespace AeroVeloz.Domain.Events.Result
{
    public sealed class OperationResult<T>
    {
        public T? Value { get; set; }
        public bool Success {get; set;}
        public string? Message { get; set;}
        public List<Object> DomainEvents { get; set; } = new();
        public static OperationResult<T> Ok (T value) => new() { Value = value, Success = true };
        public void AddEvent(Object @event) => DomainEvents.Add(@event);    

    }
}
