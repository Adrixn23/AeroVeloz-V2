using System.Collections.ObjectModel;

using AeroVeloz.Desktop.Models.DTOs.Connection;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class ConnectionListViewModel : BaseViewModel
{
    private readonly IAirportConnectionService _connectionService;
    private readonly IDialogService _dialogService;
    private readonly ConnectionDetailViewModel _detailViewModel;

    [ObservableProperty]
    private ObservableCollection<ConnectionDto> connections = new();

    [ObservableProperty]
    private ConnectionDto? selectedConnection;

    [ObservableProperty]
    private string searchText = string.Empty;

    private ObservableCollection<ConnectionDto> _allConnections = new();

    public Action? OnSavedResultAction { get; set; }

    public ConnectionListViewModel(
        IAirportConnectionService connectionService,
        IDialogService dialogService,
        ConnectionDetailViewModel detailViewModel)
    {
        _connectionService = connectionService;
        _dialogService = dialogService;
        _detailViewModel = detailViewModel;

        _detailViewModel.OnSavedResultAction += async () => await LoadConnectionsAsync();
    }

    partial void OnSearchTextChanged(string? value)
    {
        FilterConnections();
    }

    private void FilterConnections()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Connections = new ObservableCollection<ConnectionDto>(_allConnections);
        }
        else
        {
            var filtered = _allConnections
                .Where(c => c.CodeAirlinesIcao?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || c.CodeAirportIcao?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            Connections = new ObservableCollection<ConnectionDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task LoadConnectionsAsync()
    {
        IsBusy = true;
        try
        {
            var testObj = await _connectionService.GetAirportConnectionsAsync();
            var connectionList = new ObservableCollection<ConnectionDto>();
            if (testObj != null)
            {
                foreach (var c in testObj)
                {
                    connectionList.Add(c);
                }
            }
            _allConnections = connectionList;
            FilterConnections();
        }
        catch
        {
            await _dialogService.ShowErrorAsync("Error", "No se pudieron cargar las conexiones.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CreateConnectionAsync()
    {
        _detailViewModel.InitializeForCreate();
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task EditConnectionAsync(ConnectionDto? connection = null)
    {
        var target = connection ?? SelectedConnection;
        if (target == null) return;

        _detailViewModel.InitializeForEdit(target);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task DeleteConnectionAsync(ConnectionDto? connection = null)
    {
        var target = connection ?? SelectedConnection;
        if (target == null) return;

        var confirm = await _dialogService.ShowConfirmationAsync(
            $"¿Eliminar la conexión {target.CodeAirlinesIcao}-{target.CodeAirportIcao}?",
            "Confirmar eliminación");

        if (!confirm) return;

        IsBusy = true;
        try
        {
            if (Guid.TryParse(target.Id, out var connectionId))
            {
                var result = await _connectionService.DeleteConnectionAsync(connectionId);
                if (result)
                {
                    await _dialogService.ShowInfoAsync("Conexión eliminada exitosamente.");
                    await LoadConnectionsAsync();
                    OnSavedResultAction?.Invoke();
                }
                else
                {
                    await _dialogService.ShowErrorAsync("Error", "No se pudo eliminar la conexión.");
                }
            }
        }
        catch (Exception)
        {
            await _dialogService.ShowErrorAsync("Error", "Ocurrió un error inesperado al procesar la operación. Intente nuevamente.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ViewDetailsAsync(ConnectionDto? connection = null)
    {
        var target = connection ?? SelectedConnection;
        if (target == null) return;

        string details = $"ID: {target.Id}\n" +
                         $"Código ICAO Aerolínea: {target.CodeAirlinesIcao}\n" +
                         $"Código ICAO Aeropuerto: {target.CodeAirportIcao}\n" +
                         $"Activo: {(target.IsActive ? "Sí" : "No")}\n" +
                         $"Fecha de Creación: {target.CreateAt:dd/MM/yyyy}\n";
                        
        await _dialogService.ShowInfoAsync(details, "Detalles de la Conexión");
    }
}

