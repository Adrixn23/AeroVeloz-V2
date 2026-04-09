using System;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.StatusSystem;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class AirportAdminDashboardViewModel : BaseViewModel
{
    private readonly IAirportAdminStatService _statService;
    private readonly UserListViewModel _userListViewModel;
    private readonly ConnectionListViewModel _connectionListViewModel;
    private AirportAdminMainViewModel? _mainViewModel;

    [ObservableProperty]
    private AirportAdminStatsDto? airportStats;

    public Action<string>? OnNavigate { get; set; }

    public AirportAdminDashboardViewModel(
        IAirportAdminStatService statService,
        UserListViewModel userListViewModel,
        ConnectionListViewModel connectionListViewModel)
    {
        _statService = statService;
        _userListViewModel = userListViewModel;
        _connectionListViewModel = connectionListViewModel;
    }

    public void SetMainViewModel(AirportAdminMainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    [RelayCommand]
    private async Task LoadStatsAsync()
    {
        IsBusy = true;
        AirportStats = await _statService.GetAirportStatsAsync();
        IsBusy = false;
    }

    [RelayCommand]
    private async Task RegisterOperatorAsync()
    {
        IsBusy = true;
        try
        {
            _mainViewModel?.NavigateCommand.Execute("Operators");

            await Task.Delay(500);

            await _userListViewModel.CreateUserCommand.ExecuteAsync(null);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void ManageConnections()
    {
        _mainViewModel?.NavigateCommand.Execute("Connections");
    }

    [RelayCommand]
    private async Task RefreshIndicatorsAsync()
    {
        await LoadStatsAsync();
    }
}

