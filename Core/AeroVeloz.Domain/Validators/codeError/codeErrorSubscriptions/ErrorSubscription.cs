using AeroVeloz.Domain.ValidationBase;

namespace AeroVeloz.Domain.Validators.codeError.codeErrorSubscriptions
{
    public static class ErrorSubscriptions
    {
        // Errores de Creación de Suscripcion Datos minimo obligatorios

        public static DomainError InvalidFlightReference =>
            DomainError.Create("Subscription_01", "El identificador del vuelo es inválido o no fue proporcionado. Es obligatorio para seguir un vuelo.");

        public static DomainError MissingContactValue =>
            DomainError.Create("Subscription_02", "El valor de contacto (ej. correo electrónico o número de teléfono) no puede estar vacío en el flujo de visitantes.");

        public static DomainError InvalidSubscriptionChannel =>
            DomainError.Create("Subscription_03", "El canal de suscripción especificado no es válido. Debe ser un canal de transporte soportado (ej. Email, SMS).");

        public static DomainError DuplicateActiveSubscription =>
            DomainError.Create("Subscription_04", "El destinatario ya cuenta con una suscripción activa para este vuelo mediante el canal de contacto especificado.");


        // Errores de Cancelación y Ciclo de Vida en el sad se menciona: Procesos sincrónicos y estado

        public static DomainError SubscriptionNotFound =>
            DomainError.Create("Subscription_05", "No se encontró la suscripción especificada en el sistema.");

        public static DomainError SubscriptionAlreadyCanceled =>
            DomainError.Create("Subscription_06", "La operación no es válida porque la suscripción ya se encuentra inactiva o fue cancelada previamente.");

        public static DomainError FlightAlreadyClosed =>
            DomainError.Create("Subscription_07", "No es posible suscribirse a este vuelo porque su ciclo operativo ya ha finalizado (ej. Aterrizado o Cancelado).");
    }
}