using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Flight;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class FlightsForOperationListViewModel : BaseViewModel
{
    private readonly IFlightService _flightService;
    private readonly IDialogService _dialogService;
    private readonly FlightOperationDetailViewModel _detailViewModel;

    [ObservableProperty]
    private ObservableCollection<FlightForOperationDto> flights = new();

    [ObservableProperty]
    private FlightForOperationDto? selectedFlight;

    [ObservableProperty]
    private string searchText = string.Empty;

    private ObservableCollection<FlightForOperationDto> _allFlights = new();

    public Action? OnFlightSelectedAction { get; set; }

    public FlightsForOperationListViewModel(
        IFlightService flightService,
        IDialogService dialogService,
        FlightOperationDetailViewModel detailViewModel)
    {
        _flightService = flightService;
        _dialogService = dialogService;
        _detailViewModel = detailViewModel;

        _detailViewModel.OnSavedResultAction += async () => await LoadFlightsAsync();
    }

    partial void OnSearchTextChanged(string? value)
    {
        FilterFlights();
    }

    private void FilterFlights()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Flights = new ObservableCollection<FlightForOperationDto>(_allFlights);
        }
        else
        {
            var filtered = _allFlights
                .Where(f => f.FlightNumber?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || f.CodeAirlineIcao?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || f.OriginAirport?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || f.DestinationAirport?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || f.FlightStateName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            Flights = new ObservableCollection<FlightForOperationDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task LoadFlightsAsync()
    {
        IsBusy = true;
        try
        {
            var flights = await _flightService.GetFlightsForOperationsAsync();
            _allFlights = new ObservableCollection<FlightForOperationDto>(flights);
            FilterFlights();
        }
        catch
        {
            await _dialogService.ShowErrorAsync("Error", "No se pudieron cargar los vuelos.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OperateSelectedFlightAsync()
    {
        if (SelectedFlight == null)
        {
            await _dialogService.ShowErrorAsync("Advertencia", "Por favor selecciona un vuelo");
            return;
        }

        _detailViewModel.InitializeForFlightOperation(SelectedFlight);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task ViewFlightDetailsAsync(FlightForOperationDto? flight = null)
    {
        var target = flight ?? SelectedFlight;
        if (target == null) return;

        await _dialogService.ShowErrorAsync(
            "Detalles del Vuelo",
            $"Vuelo: {target.FlightNumber}\n" +
            $"Aerolínea: {target.CodeAirlineIcao}\n" +
            $"Origen: {target.OriginAirport}\n" +
            $"Destino: {target.DestinationAirport}\n" +
            $"Salida Programada: {target.ScheduledDeparture:g}\n" +
            $"Puerta: {target.BordingGate}\n" +
            $"Estado: {target.FlightStateName}\n" +
            $"Operaciones Activas: {target.ActiveOperations}/{target.TotalOperations}"
        );
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadFlightsAsync();
    }
}
