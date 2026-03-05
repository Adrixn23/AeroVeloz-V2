using AeroVeloz.Domain.ValidationBase;
namespace AeroVeloz.Domain.Validators.codeError.codeErrorNotifications
{
    public static class ErrorNotifications {

        // Errores de Validación de Suscripción
        public static DomainError InvalidSubscription =>
                DomainError.Create("Notification_01", "La Subscripcion Es invalida, fue eliminada o No fue encontrada.");

        public static DomainError SubscriptionNotActive =>
               DomainError.Create("Notification_02", "Esta subscripcion esta revocada o invalida");

        public static DomainError MissingContactDestination =>
               DomainError.Create("Notification_03", "No se puede enviar la notificación porque el destinatario no tiene configurado un punto de contacto válido para el canal seleccionado (Ej. Email o Teléfono faltante ");


        // errores de validacion de ShouldNotification

        public static DomainError InvalidNotificationState =>
              DomainError.Create("Notification_04", "La notificación se encuentra en un estado que no permite esta operación (ejemploo intentar reaccionar a un mensaje ya enviado o fallido permanentemente");

        public static DomainError FlightCycleClosed =>
              DomainError.Create("Notification_05", "El vuelo ya ha finalizado su ciclo operativo, No se admiten nuevas notificaciones automáticas.");

        public static DomainError InsignificantOperationalChange =>
          DomainError.Create("Notification_06", "El cambio operativo registrado no altera el estado visible del vuelo. No se requiere enviar una nueva notificación");


        public static DomainError ProviderServiceUnavailable=>
          DomainError.Create("Notification_07", "El servicio externo de mensajería (OneSignal, SMS, Email) no está disponible o no responde. Se requiere reintento.");


        public static DomainError ProviderPayloadRejected=>
          DomainError.Create("Notification_08", "El proveedor externo rechazó el envío de la alerta. Verifique el formato, tamaño del mensaje o los permisos de la cuenta");
        public static DomainError MessageContentEmpty =>
            DomainError.Create("Notification_09", "El contenido del mensaje de la notificación no puede estar vacío.");


        public static DomainError MissingTransportProvider =>
    DomainError.Create("Notification_10", "La notificación no tiene un proveedor de transporte asignado (Ej. Email, SMS, SignalR). Es obligatorio para el enrutamiento.");


        public static DomainError SlaTimeLimitBreached =>
    DomainError.Create("Notification_12", "La notificación superó el límite estricto de 15 minutos de retención permitido desde el cambio operativo. Alerta de desinformación crítica.");
    }
}
