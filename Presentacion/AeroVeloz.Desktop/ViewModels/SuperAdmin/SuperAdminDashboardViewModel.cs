using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class SuperAdminDashboardViewModel : BaseViewModel
{
    private readonly ISuperAdminStatService _statService;

    [ObservableProperty]
    private GlobalStatsDto? _globalStats;

    public SuperAdminDashboardViewModel(ISuperAdminStatService statService)
    {
        _statService = statService;
    }

    [RelayCommand]
    private async Task LoadStatsAsync()
    {
        IsBusy = true;
        GlobalStats = await _statService.GetGlobalStatsAsync();
        IsBusy = false;
    }
}
