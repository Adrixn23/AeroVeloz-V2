using System.Windows;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Interfaces.Auth;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class AirportAdminMainViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    private readonly AirportAdminDashboardViewModel _dashboardViewModel;
    private readonly UserListViewModel _userListViewModel;
    private readonly OperationsListViewModel _operationsListViewModel;
    private readonly ConnectionListViewModel _connectionListViewModel;
    private readonly AuditLogViewModel _auditLogViewModel;

    [ObservableProperty]
    private BaseViewModel _currentViewModel = null!;

    [ObservableProperty]
    private string _userName = "Administrador de Aeropuerto";

    [ObservableProperty]
    private string _userRole = "Airport Admin";

    [ObservableProperty]
    private string _systemDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy");

    public AirportAdminMainViewModel(
        ISessionService sessionService,
        AirportAdminDashboardViewModel dashboardViewModel,
        UserListViewModel userListViewModel,
        OperationsListViewModel operationsListViewModel,
        ConnectionListViewModel connectionListViewModel,
        AuditLogViewModel auditLogViewModel)
    {
        _sessionService = sessionService;
        _dashboardViewModel = dashboardViewModel;
        _userListViewModel = userListViewModel;
        _operationsListViewModel = operationsListViewModel;
        _connectionListViewModel = connectionListViewModel;
        _auditLogViewModel = auditLogViewModel;

        CurrentViewModel = _dashboardViewModel;
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
