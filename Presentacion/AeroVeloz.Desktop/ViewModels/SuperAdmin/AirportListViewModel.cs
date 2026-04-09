using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Airport;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class AirportListViewModel : BaseViewModel
{
    private readonly IAirportService _airportService;
    private readonly IDialogService _dialogService;
    private readonly AirportDetailViewModel _detailViewModel;

    [ObservableProperty]
    private ObservableCollection<AirportDto> _airports = new();

    private ObservableCollection<AirportDto> _allAirports = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        FilterAirports();
    }

    public AirportListViewModel(
        IAirportService airportService, 
        IDialogService dialogService,
        AirportDetailViewModel detailViewModel)
    {
        _airportService = airportService;
        _dialogService = dialogService;
        _detailViewModel = detailViewModel;
        
        _detailViewModel.OnSavedResultAction = async () => await LoadAirportsAsync();
    }

    [RelayCommand]
    public async Task LoadAirportsAsync()
    {
        IsBusy = true;
        var result = await _airportService.GetAllAsync();
        
        _allAirports.Clear();
        foreach (var item in result)
        {
            _allAirports.Add(item);
        }
        
        FilterAirports();
        IsBusy = false;
    }

    private void FilterAirports()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Airports = new ObservableCollection<AirportDto>(_allAirports);
        }
        else
        {
            var filtered = _allAirports.Where(a => 
                a.NameOrganization.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                a.Country.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                a.City.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                a.CodeAirportIata.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));
                
            Airports = new ObservableCollection<AirportDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task ShowCreateDialogAsync()
    {
        _detailViewModel.InitializeForCreate();
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task ShowEditDialogAsync(AirportDto selectedAirport)
    {
        if (selectedAirport == null) return;
        
        _detailViewModel.InitializeForEdit(selectedAirport);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task DeleteAirportAsync(AirportDto selectedAirport)
    {
        if (selectedAirport == null) return;

        var contentDialog = $"¿Estás seguro que deseas eliminar el aeropuerto '{selectedAirport.NameOrganization}'?";
        var confirm = await _dialogService.ShowConfirmationAsync(contentDialog, "Eliminar Aeropuerto");
        
        if (confirm)
        {
            IsBusy = true;
            var result = await _airportService.DeleteAsync(selectedAirport.Id);
            IsBusy = false;

            if (result)
            {
                await _dialogService.ShowInfoAsync("Aeropuerto eliminado exitosamente.");
                await LoadAirportsAsync();
            }
        }
    }
}
