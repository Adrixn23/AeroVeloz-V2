using System.Windows;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Services.Implementations.Notifications;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class SuperAdminMainViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    private readonly NotificationService _notificationService;

    [ObservableProperty]
    private BaseViewModel _currentViewModel;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _userRole = "Administrator Global";

    [ObservableProperty]
    private string _systemDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy");

    [ObservableProperty]
    private int _notificationCount = 0;

    public ObservableCollection<string> Notifications { get; } = new();

    private readonly SuperAdminDashboardViewModel _dashboardViewModel;
    private readonly AirportListViewModel _airportsViewModel;

    private readonly AdminListViewModel _adminsViewModel;

    public SuperAdminMainViewModel(
        ISessionService sessionService,
        NotificationService notificationService,
        SuperAdminDashboardViewModel dashboardViewModel,
        AirportListViewModel airportsViewModel,
        AdminListViewModel adminsViewModel)
    {
        _sessionService = sessionService;
        _notificationService = notificationService;
        _dashboardViewModel = dashboardViewModel;
        _airportsViewModel = airportsViewModel;
        _adminsViewModel = adminsViewModel;
        _currentViewModel = _dashboardViewModel;

        UserName = _sessionService.UserName ?? "Super Admin";

        _notificationService.OnNotificationReceived = (title, message) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Notifications.Insert(0, $"{title}: {message}");
                NotificationCount++;
            });
        };

        _ = _notificationService.StartAsync();
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
    private async void Logout(Window currentWindow)
    {
        await _notificationService.StopAsync();
        _sessionService.ClearSession();
        var loginView = App.AppHost?.Services.GetRequiredService<LoginView>();
        if (loginView != null)
        {
            loginView.Show();
            currentWindow?.Close();
        }
    }
}
