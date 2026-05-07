using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Integrations.Notifications.Validation
{
    /// <summary>
    /// Valida que la configuración SMTP esté completa y correcta.
    /// Lanza excepciones si falta información crítica en ambiente Development.
    /// </summary>
    public static class SmtpConfigurationValidator
    {
        public static void ValidateConfiguration(IConfiguration configuration, ILogger logger)
        {
            var environment = configuration["ASPNETCORE_ENVIRONMENT"] ?? "Production";
            
            var host = configuration["Smtp:Host"];
            var port = configuration["Smtp:Port"];
            var userName = configuration["Smtp:UserName"];
            var password = configuration["Smtp:Password"];
            var fromAddress = configuration["Smtp:FromAddress"];
            var enableSsl = configuration["Smtp:EnableSsl"];

            var missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(host)) missingFields.Add("Smtp:Host");
            if (string.IsNullOrWhiteSpace(port)) missingFields.Add("Smtp:Port");
            if (string.IsNullOrWhiteSpace(userName)) missingFields.Add("Smtp:UserName");
            if (string.IsNullOrWhiteSpace(password)) missingFields.Add("Smtp:Password");
            if (string.IsNullOrWhiteSpace(fromAddress)) missingFields.Add("Smtp:FromAddress");
            if (string.IsNullOrWhiteSpace(enableSsl)) missingFields.Add("Smtp:EnableSsl");

            if (missingFields.Count > 0)
            {
                var message = $"SMTP Configuration Error: Los siguientes campos están vacíos: {string.Join(", ", missingFields)}\n" +
                    "Por favor configura los User Secrets ejecutando: SETUP-USER-SECRETS.ps1";

                logger.LogError(message);

                if (environment == "Development")
                {
                    logger.LogWarning("⚠️  En Development: Los emails NO se enviarán hasta que configures los User Secrets");
                }
                else
                {
                    throw new InvalidOperationException(message);
                }
            }
            else
            {
                logger.LogInformation("✅ SMTP Configuration válida y completa");
            }
        }
    }
}
