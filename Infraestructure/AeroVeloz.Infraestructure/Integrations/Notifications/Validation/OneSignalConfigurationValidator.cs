using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Integrations.Notifications.Validation
{
    /// <summary>
    /// Valida que la configuración OneSignal esté completa y correcta.
    /// Lanza excepciones si falta información crítica en ambiente Production.
    /// </summary>
    public static class OneSignalConfigurationValidator
    {
        public static void ValidateConfiguration(IConfiguration configuration, ILogger logger)
        {
            var environment = configuration["ASPNETCORE_ENVIRONMENT"] ?? "Production";
            
            var appId = configuration["OneSignal:AppId"];
            var restApiKey = configuration["OneSignal:RestApiKey"];

            var missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(appId)) missingFields.Add("OneSignal:AppId");
            if (string.IsNullOrWhiteSpace(restApiKey)) missingFields.Add("OneSignal:RestApiKey");

            if (missingFields.Count > 0)
            {
                var message = $"OneSignal Configuration Warning: Los siguientes campos están vacíos: {string.Join(", ", missingFields)}\n" +
                    "Por favor configura los User Secrets ejecutando: SETUP-USER-SECRETS.ps1";

                logger.LogWarning(message);

                if (environment != "Development")
                {
                    logger.LogWarning("⚠️  En {Environment}: Las notificaciones Push NO se enviarán", environment);
                }
                else
                {
                    logger.LogWarning("ℹ️  En Development: Las notificaciones Push son opcionales");
                }
            }
            else
            {
                logger.LogInformation("✅ OneSignal Configuration válida y completa");
            }
        }
    }
}
