using System.Windows;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class SuperAdminMainViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;

    [ObservableProperty]
    private BaseViewModel _currentViewModel;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _userRole = "Administrator Global";

    [ObservableProperty]
    private string _systemDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy");

    private readonly SuperAdminDashboardViewModel _dashboardViewModel;
    private readonly AirportListViewModel _airportsViewModel;

    private readonly AdminListViewModel _adminsViewModel;

    public SuperAdminMainViewModel(
        ISessionService sessionService,
        SuperAdminDashboardViewModel dashboardViewModel,
        AirportListViewModel airportsViewModel,
        AdminListViewModel adminsViewModel)
    {
        _sessionService = sessionService;
        _dashboardViewModel = dashboardViewModel;
        _airportsViewModel = airportsViewModel;
        _adminsViewModel = adminsViewModel;
        _currentViewModel = _dashboardViewModel;

        UserName = _sessionService.UserName ?? "Super Admin";
    }

    [RelayCommand]
    private void NavigateToDashboard()
    {
        _currentViewModel = _dashboardViewModel;
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    [RelayCommand]
    private void NavigateToAirports()
    {
        _currentViewModel = _airportsViewModel;
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    [RelayCommand]
    private void NavigateToAdmins()
    {
        _currentViewModel = _adminsViewModel;
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    [RelayCommand]
    private void Logout(Window currentWindow)
    {
        _sessionService.ClearSession();
        var loginView = App.AppHost?.Services.GetRequiredService<LoginView>();

        if (loginView != null)
        {
            loginView.Show();
            currentWindow?.Close();
        }
    }
}
