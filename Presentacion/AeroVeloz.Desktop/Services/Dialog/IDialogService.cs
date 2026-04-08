using System.Threading.Tasks;

namespace AeroVeloz.Desktop.Services.Dialog;

public interface IDialogService
{
    Task ShowErrorAsync(string message, string title = "Error");
    Task ShowInfoAsync(string message, string title = "Información");
    Task ShowWarningAsync(string message, string title = "Advertencia");
    Task<bool> ShowConfirmationAsync(string message, string title = "Confirmación");
}
