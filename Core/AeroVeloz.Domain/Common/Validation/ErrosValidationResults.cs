namespace AeroVeloz.Domain.Common.Validation
{
    /// <summary>
    /// Clase sellada e inmutable que representa un error de validación del dominio.
    /// Cada error se compone de un código único y una descripción legible.
    /// de reglas de negocio de forma estructurada.
    /// </summary>
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
