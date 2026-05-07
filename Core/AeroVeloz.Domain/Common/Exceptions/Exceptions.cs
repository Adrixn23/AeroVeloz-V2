namespace AeroVeloz.Domain.Common.Exceptions
{
    public class ExceptionBase : Exception
    {
        public string Code { get; }

        public ExceptionBase(string code, string message, Exception? innerException = null)
            : base(message, innerException)
        {
            Code = code;
        }
    }

    public class DatabaseOperationException : ExceptionBase
    {
        public DatabaseOperationException(string message, Exception? innerException = null)
            : base("DB_OP_ERR", message, innerException)
        {
        }
    }

    public class EntityNotFoundException : ExceptionBase
    {
        public EntityNotFoundException(string entityName, object key)
            : base("NOT_FOUND_ERR", $"La entidad de tipo '{entityName}' con identificador '{key}' no fue encontrada.")
        {
        }
    }
}
