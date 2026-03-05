namespace AeroVeloz.Domain.Events.Result
{
    public sealed class OperationResult<T> //este elemento es usado entre las diversas capas  y acciones que requieren un
        //return operational para notificar a los diveros interesados dentro del sistema
    {
        public T? Value { get; private set; }
        
        public bool Success {get; private set;}

        public string? Message { get; private set;}

        public string? errorCode { get; private set; }


        public List<Object> DomainEvents { get; private set; } = new();

        public static OperationResult<T> Ok (T value) => new() { Value = value, Success = true };
       
        public void AddEvent(Object @event) => DomainEvents.Add(@event);
        
        //agregar elemento de operation result para los fallos del sistema 
        //es decir registrar cuando no se pudo lanzar el evento por x problema para notificar entonces
        // al elemento que intento realizar la accion en cuestion
 
    }
}
