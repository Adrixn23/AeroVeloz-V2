using System.Windows;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Services.Implementations.Notifications;

namespace AeroVeloz.Desktop.ViewModels.OperationalUser;

public partial class OperationalUserMainViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    private readonly NotificationService _notificationService;
    private readonly OperationalUserDashboardViewModel _dashboardViewModel;
    private readonly OperationsListViewModel _operationsListViewModel;
    private readonly ConnectionListViewModel _connectionListViewModel;
    private readonly AeroVeloz.Desktop.ViewModels.OperationalUser.AirlineListViewModel _airlineListViewModel;

    [ObservableProperty]
    private BaseViewModel _currentViewModel = null!;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _userRole = "Operador de Aeropuerto";

    [ObservableProperty]
    private string _systemDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy");

    [ObservableProperty]
    private int _notificationCount = 0;

    public ObservableCollection<string> Notifications { get; } = new();

    public OperationalUserMainViewModel(
        ISessionService sessionService,
        NotificationService notificationService,
        OperationalUserDashboardViewModel dashboardViewModel,
        OperationsListViewModel operationsListViewModel,
        ConnectionListViewModel connectionListViewModel,
        AeroVeloz.Desktop.ViewModels.OperationalUser.AirlineListViewModel airlineListViewModel)
    {
        _sessionService = sessionService;
        _notificationService = notificationService;
        _dashboardViewModel = dashboardViewModel;
        _operationsListViewModel = operationsListViewModel;
        _connectionListViewModel = connectionListViewModel;
        _airlineListViewModel = airlineListViewModel;

        _dashboardViewModel.OnNavigate = destination => NavigateTo(destination);

        UserName = _sessionService.UserName ?? "Operador";
        CurrentViewModel = _dashboardViewModel;

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
    private void NavigateTo(string viewName)
    {
        switch (viewName)
        {
            case "Dashboard":
                CurrentViewModel = _dashboardViewModel;
                break;
            case "Operations":
                CurrentViewModel = _operationsListViewModel;
                break;
            case "Connections":
                CurrentViewModel = _connectionListViewModel;
                break;
            case "Airlines":
                CurrentViewModel = _airlineListViewModel;
                break;
        }
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
