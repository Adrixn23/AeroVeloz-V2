using System.Net;
using System.Net.Mail;
using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AeroVeloz.Infraestructure.Integrations.Email
{
    public class SmtpEmailChannel : INotificationChannel
    {
        private readonly SmtpEmailOptions _options;
        private readonly ILogger<SmtpEmailChannel> _logger;

        public ChannelType Channel => ChannelType.Email;

        public SmtpEmailChannel(IOptions<SmtpEmailOptions> options, ILogger<SmtpEmailChannel> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendAsync(NotificationPayload payload)
        {
            if (string.IsNullOrWhiteSpace(payload.EmailAddress))
            {
                _logger.LogWarning("No se puede enviar email: dirección de correo no proporcionada para '{Title}'", payload.Title);
                return;
            }

            try
            {
                using var client = new SmtpClient(_options.Host, _options.Port)
                {
                    Credentials = new NetworkCredential(_options.UserName, _options.Password),
                    EnableSsl = _options.EnableSsl
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_options.FromAddress, _options.FromName),
                    Subject = payload.Title,
      
                    Body = BuildEmailBody(payload),
                    IsBodyHtml = true
                };

              

                message.To.Add(payload.EmailAddress);

                await client.SendMailAsync(message);

                _logger.LogInformation("Email enviado exitosamente a {Email} - {Title}", payload.EmailAddress, payload.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar email a {Email} - {Title}", payload.EmailAddress, payload.Title);
            }
        }

        private static string BuildEmailBody(NotificationPayload payload)
        {
            return $"""
                <html>
                <body style="font-family: Arial, sans-serif;">
                    <h2>{payload.Title}</h2>
                    <p>{payload.Message}</p>
                    {(string.IsNullOrWhiteSpace(payload.Detail) ? "" : $"<p><strong>Detalle:</strong> {payload.Detail}</p>")}
                    <hr/>
                    <p style="color: #888; font-size: 12px;">AeroVeloz - Sistema de Gestión Aeroportuaria</p>
                </body>
                </html>
                """;
        }
    }
}
