using System.Windows;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.ViewModels.Base;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;
using AeroVeloz.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.Desktop.ViewModels.OperationalUser;

public partial class OperationalUserMainViewModel : BaseViewModel
{
    private readonly ISessionService _sessionService;
    private readonly OperationalUserDashboardViewModel _dashboardViewModel;
    private readonly OperationsListViewModel _operationsListViewModel;

    [ObservableProperty]
    private BaseViewModel _currentViewModel = null!;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _userRole = "Operador de Aeropuerto";

    [ObservableProperty]
    private string _systemDate = System.DateTime.Now.ToString("dddd, dd MMMM yyyy");

    public OperationalUserMainViewModel(
        ISessionService sessionService,
        OperationalUserDashboardViewModel dashboardViewModel,
        OperationsListViewModel operationsListViewModel)
    {
        _sessionService = sessionService;
        _dashboardViewModel = dashboardViewModel;
        _operationsListViewModel = operationsListViewModel;

        UserName = _sessionService.UserName ?? "Operador";
        CurrentViewModel = _dashboardViewModel;
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
