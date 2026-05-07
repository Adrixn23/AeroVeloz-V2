using AeroVeloz.Desktop.Models.DTOs.StatusSystem;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using AeroVeloz.Desktop.Services.Interfaces.AdminSystem;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class SuperAdminDashboardViewModel : BaseViewModel
{
    private readonly ISuperAdminStatService _statService;
    private readonly AirportDetailViewModel _airportDetailViewModel;
    private readonly AdminDetailViewModel _adminDetailViewModel;

    [ObservableProperty]
    private GlobalStatsDto? _globalStats;

    public Action<string>? OnNavigateRequested;

    [RelayCommand]
    private void NavigateTo(string destination)
    {
        OnNavigateRequested?.Invoke(destination);
    }

    public SuperAdminDashboardViewModel(
        ISuperAdminStatService statService,
        AirportDetailViewModel airportDetailViewModel,
        AdminDetailViewModel adminDetailViewModel)
    {
        _statService = statService;
        _airportDetailViewModel = airportDetailViewModel;
        _adminDetailViewModel = adminDetailViewModel;

        _airportDetailViewModel.OnSavedResultAction += async () => await LoadStatsAsync();
        _adminDetailViewModel.OnSavedResultAction += async () => await LoadStatsAsync();
    }

    [RelayCommand]
    private async Task LoadStatsAsync()
    {
        IsBusy = true;
        GlobalStats = await _statService.GetGlobalStatsAsync();
        IsBusy = false;
    }

    [RelayCommand]
    private async Task OpenCreateAirportDialogAsync()
    {
        _airportDetailViewModel.InitializeForCreate();
        await DialogHost.Show(_airportDetailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task OpenCreateAdminDialogAsync()
    {
        _adminDetailViewModel.InitializeForCreate();
        await DialogHost.Show(_adminDetailViewModel, "RootDialog");
    }
}
