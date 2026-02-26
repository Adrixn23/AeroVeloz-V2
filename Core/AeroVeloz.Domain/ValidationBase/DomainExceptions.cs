namespace AeroVeloz.Domain.ValidationBase
{
    public abstract class DomainExceptions  : Exception
    {
        protected DomainExceptions(string message) : base(message) { }  //clase base que maneja las exceptions (validators de campos)
    }
}
