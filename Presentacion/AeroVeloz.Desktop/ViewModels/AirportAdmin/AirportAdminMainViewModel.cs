using System.Windows;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Services.Implementations.Notifications;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class AirportAdminMainViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    private readonly NotificationService _notificationService;
    private readonly AirportAdminDashboardViewModel _dashboardViewModel;
    private readonly UserListViewModel _userListViewModel;
    private readonly OperationsListViewModel _operationsListViewModel;
    private readonly ConnectionListViewModel _connectionListViewModel;
    private readonly AuditLogViewModel _auditLogViewModel;

    [ObservableProperty]
    private BaseViewModel _currentViewModel = null!;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _userRole = "Airport Admin";

    [ObservableProperty]
    private string _systemDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy");

    [ObservableProperty]
    private int _notificationCount = 0;

    public ObservableCollection<string> Notifications { get; } = new();

    public AirportAdminMainViewModel(
        ISessionService sessionService,
        NotificationService notificationService,
        AirportAdminDashboardViewModel dashboardViewModel,
        UserListViewModel userListViewModel,
        OperationsListViewModel operationsListViewModel,
        ConnectionListViewModel connectionListViewModel,
        AuditLogViewModel auditLogViewModel)
    {
        _sessionService = sessionService;
        _notificationService = notificationService;
        _dashboardViewModel = dashboardViewModel;
        _userListViewModel = userListViewModel;
        _operationsListViewModel = operationsListViewModel;
        _connectionListViewModel = connectionListViewModel;
        _auditLogViewModel = auditLogViewModel;

        CurrentViewModel = _dashboardViewModel;

        UserName = _sessionService.UserName ?? "Administrador de Aeropuerto";

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
    private void Navigate(string viewName)
    {
        switch (viewName)
        {
            case "Dashboard":
                CurrentViewModel = _dashboardViewModel;
                break;
            case "Operators":
                CurrentViewModel = _userListViewModel;
                break;
            case "Operations":
                CurrentViewModel = _operationsListViewModel;
                break;
            case "Connections":
                CurrentViewModel = _connectionListViewModel;
                break;
            case "Audit":
                CurrentViewModel = _auditLogViewModel;
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
