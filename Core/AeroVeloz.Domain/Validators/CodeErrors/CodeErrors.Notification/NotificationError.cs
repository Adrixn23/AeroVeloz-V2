using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Notification
{
    public static class NotificationError
    {
        public static DomainError InvalidNotification =>
            DomainError.Create("NO_01", "La notificacion que se ha intentado lanzar a sufrido un error, posible valores nulos o fallos en servicios de mensajeria");

        public static DomainError InvalidProvider =>
            DomainError.Create("NO_02", "El proveedor de mensajeria por el cual se intento envia el mensaje no ha respondido la solicitud, intente con otro servicio de mensajeria");

        public static DomainError InvalidMessage =>
            DomainError.Create("NO_03", "El mensaje que se a intentado enviar se encuentra en un estaod invalido, posible valor nulo o no comprendido por el sistema");

        public static DomainError InvalidSubscription =>
            DomainError.Create("NO_04", "La notificación que se ha intentado hacer no corresponde a una subscripcion registrad. ");
    }
}
