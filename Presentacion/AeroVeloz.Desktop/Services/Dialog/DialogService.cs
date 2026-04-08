using System.Threading.Tasks;
using System.Windows;

namespace AeroVeloz.Desktop.Services.Dialog;

public class DialogService : IDialogService
{
    public Task ShowErrorAsync(string message, string title = "Error")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        return Task.CompletedTask;
    }

    public Task ShowInfoAsync(string message, string title = "Información")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        return Task.CompletedTask;
    }

    public Task ShowWarningAsync(string message, string title = "Advertencia")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        return Task.CompletedTask;
    }

    public Task<bool> ShowConfirmationAsync(string message, string title = "Confirmación")
    {
        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        return Task.FromResult(result == MessageBoxResult.Yes);
    }
}
