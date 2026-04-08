using System.Collections.ObjectModel;

using AeroVeloz.Desktop.Models.DTOs.AdminControl;
using AeroVeloz.Desktop.Models.DTOs.Airport;
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class AdminListViewModel : BaseViewModel
{
    private readonly IAdminManagerService _adminManagerService;
    private readonly IAirportService _airportService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private ObservableCollection<UserDto> _availableAdmins = new();

    private ObservableCollection<UserDto> _allAdmins = new();

    [ObservableProperty]
    private ObservableCollection<AirportDto> _airports = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AssignAdminCommand))]
    private UserDto? _selectedAdmin;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AssignAdminCommand))]
    private AirportDto? _selectedAirport;

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string? value)
    {
        FilterAdmins();
    }

    public AdminListViewModel(
        IAdminManagerService adminManagerService,
        IAirportService airportService,
        IDialogService dialogService)
    {
        _adminManagerService = adminManagerService;
        _airportService = airportService;
        _dialogService = dialogService;
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        IsBusy = true;
        
        var adminsTask = _adminManagerService.GetAvailableAdminsAsync();
        var airportsTask = _airportService.GetAllAsync();

        await Task.WhenAll(adminsTask, airportsTask);

        _allAdmins.Clear();
        foreach (var admin in adminsTask.Result)
        {
            _allAdmins.Add(admin);
        }

        Airports.Clear();
        foreach (var airport in airportsTask.Result)
        {
            Airports.Add(airport);
        }

        FilterAdmins();
        IsBusy = false;
    }

    private void FilterAdmins()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            AvailableAdmins = new ObservableCollection<UserDto>(_allAdmins);
        }
        else
        {
            var filtered = _allAdmins.Where(a => 
                (a.FullName?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.Email?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false));
                
            AvailableAdmins = new ObservableCollection<UserDto>(filtered);
        }
    }

    [RelayCommand(CanExecute = nameof(CanAssignAdmin))]
    private async Task AssignAdminAsync()
    {
        if (SelectedAdmin == null || SelectedAirport == null) return;

        IsBusy = true;
        var dto = new AssignAdminDto
        {
            UserId = SelectedAdmin.Id,
            AirportId = SelectedAirport.Id
        };

        var success = await _adminManagerService.AssignAdminToAirportAsync(dto);
        IsBusy = false;

        if (success)
        {
            await _dialogService.ShowInfoAsync($"Se asignó correctamente a {SelectedAdmin.FullName} al aeropuerto de {SelectedAirport.NameOrganization}.", "Asignación Exitosa");
            SelectedAdmin = null;
            SelectedAirport = null;
            await LoadDataAsync();
        }
    }

    private bool CanAssignAdmin()
    {
        return SelectedAdmin != null && SelectedAirport != null && !IsBusy;
    }
}
