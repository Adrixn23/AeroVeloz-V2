using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.codeError.codeErrorNotifications
{
    public static class ErrorNotifications {

        // Errores de Validación de Suscripción
        public static ErrosValidationResults InvalidSubscription =>
                ErrosValidationResults.Create("Notification_01", "La Subscripcion Es invalida, fue eliminada o No fue encontrada.");

        public static ErrosValidationResults SubscriptionNotActive =>
               ErrosValidationResults.Create("Notification_02", "Esta subscripcion esta revocada o invalida");

        public static ErrosValidationResults MissingContactDestination =>
               ErrosValidationResults.Create("Notification_03", "No se puede enviar la notificación porque el destinatario no tiene configurado un punto de contacto válido para el canal seleccionado (Ej. Email o Teléfono faltante ");


        // errores de validacion de ShouldNotification

        public static ErrosValidationResults InvalidNotificationState =>
              ErrosValidationResults.Create("Notification_04", "La notificación se encuentra en un estado que no permite esta operación (ejemploo intentar reaccionar a un mensaje ya enviado o fallido permanentemente");

        public static ErrosValidationResults FlightCycleClosed =>
              ErrosValidationResults.Create("Notification_05", "El vuelo ya ha finalizado su ciclo operativo, No se admiten nuevas notificaciones automáticas.");

        public static ErrosValidationResults InsignificantOperationalChange =>
          ErrosValidationResults.Create("Notification_06", "El cambio operativo registrado no altera el estado visible del vuelo. No se requiere enviar una nueva notificación");


        public static ErrosValidationResults ProviderServiceUnavailable =>
          ErrosValidationResults.Create("Notification_07", "El servicio externo de mensajería (OneSignal, SMS, Email) no está disponible o no responde. Se requiere reintento.");


        public static ErrosValidationResults ProviderPayloadRejected =>
          ErrosValidationResults.Create("Notification_08", "El proveedor externo rechazó el envío de la alerta. Verifique el formato, tamaño del mensaje o los permisos de la cuenta");
        public static ErrosValidationResults MessageContentEmpty =>
            ErrosValidationResults.Create("Notification_09", "El contenido del mensaje de la notificación no puede estar vacío.");


        public static ErrosValidationResults MissingTransportProvider =>
    ErrosValidationResults.Create("Notification_10", "La notificación no tiene un proveedor de transporte asignado (Ej. Email, SMS, SignalR). Es obligatorio para el enrutamiento.");


        public static ErrosValidationResults SlaTimeLimitBreached =>
    ErrosValidationResults.Create("Notification_12", "La notificación superó el límite estricto de 15 minutos de retención permitido desde el cambio operativo. Alerta de desinformación crítica.");
    }
}
