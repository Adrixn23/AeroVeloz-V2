using AeroVeloz.Desktop.Models.DTOs.Connection;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class ConnectionDetailViewModel : BaseViewModel
{
    private readonly IAirportConnectionService _connectionService;
    private readonly IDialogService _dialogService;
    private bool _isEditMode;
    private Guid _currentConnectionId;

    [ObservableProperty]
    private string title = "Crear Conexión";

    [ObservableProperty]
    private string codeAirlinesIcao = string.Empty;

    [ObservableProperty]
    private string codeAirportIcao = string.Empty;

    [ObservableProperty]
    private bool isActive = true;

    public Action? OnSavedResultAction { get; set; }

    public ConnectionDetailViewModel(
        IAirportConnectionService connectionService,
        IDialogService dialogService)
    {
        _connectionService = connectionService;
        _dialogService = dialogService;
    }

    public void InitializeForCreate()
    {
        _isEditMode = false;
        Title = "Crear Nueva Conexión";
        CodeAirlinesIcao = string.Empty;
        CodeAirportIcao = string.Empty;
        IsActive = true;
    }

    public void InitializeForEdit(ConnectionDto connection)
    {
        _isEditMode = true;
        Title = $"Editar Conexión: {connection.CodeAirlinesIcao}-{connection.CodeAirportIcao}";
        if (Guid.TryParse(connection.Id, out var id))
        {
            _currentConnectionId = id;
        }
        CodeAirlinesIcao = connection.CodeAirlinesIcao ?? string.Empty;
        CodeAirportIcao = connection.CodeAirportIcao ?? string.Empty;
        IsActive = connection.IsActive;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(CodeAirlinesIcao))
        {
            await _dialogService.ShowInfoAsync("Debe ingresar el código ICAO de la aerolínea.", "Validación");
            return;
        }

        if (string.IsNullOrWhiteSpace(CodeAirportIcao))
        {
            await _dialogService.ShowInfoAsync("Debe ingresar el código ICAO del aeropuerto.", "Validación");
            return;
        }

        IsBusy = true;
        bool result = false;

        try
        {
            var connectionData = new
            {
                CodeAirlinesIcao,
                CodeAirportIcao,
                IsActive
            };

            if (_isEditMode)
            {
                result = await _connectionService.UpdateConnectionAsync(_currentConnectionId, connectionData);
            }
            else
            {
                result = await _connectionService.CreateConnectionAsync(connectionData);
            }

            IsBusy = false;

            if (result)
            {
                await _dialogService.ShowInfoAsync("Conexión guardada exitosamente.");
                DialogHost.CloseDialogCommand.Execute(null, null);
                OnSavedResultAction?.Invoke();
            }
            else
            {
                await _dialogService.ShowInfoAsync("Error al guardar la conexión. Intente nuevamente.", "Error");
            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await _dialogService.ShowErrorAsync("Error", $"Ocurrió un error: {ex.Message}");
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        DialogHost.CloseDialogCommand.Execute(null, null);
    }
}
