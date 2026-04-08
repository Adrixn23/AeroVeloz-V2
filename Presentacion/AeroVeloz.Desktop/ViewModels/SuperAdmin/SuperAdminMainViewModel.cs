using System.Windows;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class SuperAdminMainViewModel : BaseViewModel
{
    [ObservableProperty]
    private BaseViewModel _currentViewModel;

    [ObservableProperty]
    private string _userName = "Super Admin";

    [ObservableProperty]
    private string _userRole = "Administrator Global";

    private readonly SuperAdminDashboardViewModel _dashboardViewModel;
    private readonly AirportListViewModel _airportsViewModel;

    private readonly AdminListViewModel _adminsViewModel;

    public SuperAdminMainViewModel(
        SuperAdminDashboardViewModel dashboardViewModel,
        AirportListViewModel airportsViewModel,
        AdminListViewModel adminsViewModel)
    {
        _dashboardViewModel = dashboardViewModel;
        _airportsViewModel = airportsViewModel;
        _adminsViewModel = adminsViewModel;
        _currentViewModel = _dashboardViewModel;
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
        var loginView = App.AppHost?.Services.GetRequiredService<LoginView>();
        
        if (loginView != null)
        {
            loginView.Show();
            currentWindow?.Close();
        }
    }
}
