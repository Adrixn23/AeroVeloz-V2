namespace AeroVeloz.Domain.Common.Validation
{
    public sealed class ErrosValidationResults
    {
        public string? code { get;}
        public string? description { get; }

        private ErrosValidationResults(string code, string description) 
        {
            this.code = code; 
            this.description = description;
        }

        public static ErrosValidationResults Create(string code, string description)
            => new(code, description);
    }
}
