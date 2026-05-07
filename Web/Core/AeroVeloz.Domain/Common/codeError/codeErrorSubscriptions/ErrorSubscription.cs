using AeroVeloz.Domain.Common.Validation;


namespace AeroVeloz.Domain.Common.codeError.codeErrorSubscriptions
{
    public static class ErrorSubscriptions
    {
        // Errores de Creación de Suscripcion Datos minimo obligatorios

        public static ErrosValidationResults InvalidFlightReference =>
            ErrosValidationResults.Create("Subscription_01", "El identificador del vuelo es inválido o no fue proporcionado. Es obligatorio para seguir un vuelo.");

        public static ErrosValidationResults MissingContactValue =>
            ErrosValidationResults.Create("Subscription_02", "El valor de contacto (ej. correo electrónico o número de teléfono) no puede estar vacío en el flujo de visitantes.");

        public static ErrosValidationResults InvalidSubscriptionChannel =>
            ErrosValidationResults.Create("Subscription_03", "El canal de suscripción especificado no es válido. Debe ser un canal de transporte soportado (ej. Email, SMS).");

        public static ErrosValidationResults DuplicateActiveSubscription =>
            ErrosValidationResults.Create("Subscription_04", "El destinatario ya cuenta con una suscripción activa para este vuelo mediante el canal de contacto especificado.");


        // Errores de Cancelación y Ciclo de Vida en el sad se menciona: Procesos sincrónicos y estado

        public static ErrosValidationResults SubscriptionNotFound =>
            ErrosValidationResults.Create("Subscription_05", "No se encontró la suscripción especificada en el sistema.");

        public static ErrosValidationResults SubscriptionAlreadyCanceled =>
            ErrosValidationResults.Create("Subscription_06", "La operación no es válida porque la suscripción ya se encuentra inactiva o fue cancelada previamente.");

        public static ErrosValidationResults FlightAlreadyClosed =>
            ErrosValidationResults.Create("Subscription_07", "No es posible suscribirse a este vuelo porque su ciclo operativo ya ha finalizado (ej. Aterrizado o Cancelado).");
    }
}