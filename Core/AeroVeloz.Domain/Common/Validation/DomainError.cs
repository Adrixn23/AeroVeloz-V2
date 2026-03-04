namespace AeroVeloz.Domain.Common.Validation
{
    public sealed class DomainError
    {
        private string? code { get;}
        private string? description { get; }

        private DomainError(string code, string description) 
        {
            this.code = code; 
            this.description = description;
        }

        public static DomainError Create(string code, string description)
            => new(code, description);
    }
}
