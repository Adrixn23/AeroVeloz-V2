using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Connection;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class AirlineListViewModel : BaseViewModel
{
    private readonly IAirportConnectionService _airportConnectionService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private ObservableCollection<ConnectionDto> _connectedAirlines = new();

    [ObservableProperty]
    private ConnectionDto? _selectedConnection;

    private ObservableCollection<ConnectionDto> _allConnectedAirlines = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        FilterAirlines();
    }

    public AirlineListViewModel(IAirportConnectionService airportConnectionService, IDialogService dialogService)
    {
        _airportConnectionService = airportConnectionService;
        _dialogService = dialogService;
    }

    [RelayCommand]
    public async Task LoadConnectedAirlinesAsync()
    {
        IsBusy = true;
        var result = await _airportConnectionService.GetAirportConnectionsAsync();

        _allConnectedAirlines.Clear();
        foreach (var item in result)
        {
            _allConnectedAirlines.Add(item);
        }

        FilterAirlines();
        IsBusy = false;
    }

    private void FilterAirlines()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            ConnectedAirlines = new ObservableCollection<ConnectionDto>(_allConnectedAirlines);
        }
        else
        {
            var filtered = _allConnectedAirlines.Where(a => 
                (a.AirlineName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.CodeAirlinesIcao?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));

            ConnectedAirlines = new ObservableCollection<ConnectionDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task RefreshAirlinesAsync()
    {
        await LoadConnectedAirlinesAsync();
    }
    [RelayCommand]
    private async Task ViewDetailsAsync(ConnectionDto connection)
    {
        if (connection == null) return;

        string details = "Aerolínea: " + connection.AirlineName + "\n" +
                         "Código ICAO: " + connection.CodeAirlinesIcao + "\n" +
                         "Activo: " + (connection.IsActive ? "Sí" : "No") + "\n" +
                         "Fecha de Registro: " + connection.CreateAt.ToString("dd/MM/yyyy");

        await _dialogService.ShowInfoAsync(details, "Detalles de la Aerolínea");
    }
}
