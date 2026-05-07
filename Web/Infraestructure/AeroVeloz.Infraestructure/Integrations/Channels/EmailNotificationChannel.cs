using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace AeroVeloz.Infraestructure.Integrations.Channels
{
    public class EmailNotificationChannel : INotificationChannel
    {
        private readonly ILogger<EmailNotificationChannel> _logger;
        private readonly IConfiguration _configuration;

        public EmailNotificationChannel(ILogger<EmailNotificationChannel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public ChannelType Channel => ChannelType.Email;

        public async Task SendAsync(NotificationPayload payload)
        {
            var server = _configuration["SmtpSettings:Server"];
            if (string.IsNullOrEmpty(server))
            {
                _logger.LogWarning("SMTP no configurado. Notificación enviada solo a log.");
                return;
            }

            try
            {
                // Forzar protocolo de seguridad moderno para Google
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                using var client = new SmtpClient(server, int.Parse(_configuration["SmtpSettings:Port"]!))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        _configuration["SmtpSettings:Username"], 
                        _configuration["SmtpSettings:Password"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 20000 // 10 segundos
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["SmtpSettings:SenderEmail"]!, _configuration["SmtpSettings:SenderName"]),
                    Subject = payload.Title,
                    Body = $@"
                        <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #3b82f6; border-radius: 12px;'>
                            <h2 style='color: #3b82f6;'>AeroVeloz - Confirmación</h2>
                            <p style='font-size: 1.1rem;'>{payload.Message}</p>
                            <br />
                            <div style='background: #f8fafc; padding: 15px; border-radius: 8px;'>
                                <strong>Detalle del Sistema:</strong><br />
                                Notificación generada el: {DateTime.Now:f}
                            </div>
                        </div>",
                    IsBodyHtml = true
                };

                foreach (var target in payload.TargetExternalIds)
                {
                    if (!string.IsNullOrWhiteSpace(target))
                        mailMessage.To.Add(target.Trim());
                }

                _logger.LogInformation("Intentando enviar correo real a {Targets} via Gmail...", string.Join(",", payload.TargetExternalIds));
                await client.SendMailAsync(mailMessage);
                _logger.LogInformation("¡ÉXITO! Correo entregado a los servidores de Google.");
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError($"ERROR SMTP DE GOOGLE: {smtpEx.Message} | StatusCode: {smtpEx.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GENERAL DE CORREO: " + ex.Message);
            }
        }
    }
}
