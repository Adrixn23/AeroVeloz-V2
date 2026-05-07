namespace AeroVeloz.Application.DTOs.Notifications
{
    public sealed record NotificationReadDto(
        Guid Id,
        string Title,        // titulo principal, ejm vuelo retrasado
        string Message,      // descripcion de la noti
        DateTime CreatedAt,  // Para mostrar hace cuanto llegoo
        bool IsRead,         // Para saber si esta leido o no
        short? FlightNumber  // Opcional: para que al darle clic, lo lleve a la pantalla del vuelo
    );
}