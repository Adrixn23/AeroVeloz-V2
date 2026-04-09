using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using AeroVeloz.Desktop.Views.SuperAdmin;
using AeroVeloz.Desktop.Views.AirportAdmin;
using AeroVeloz.Desktop.Views.OperationalUser;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Interfaces.Auth;

namespace AeroVeloz.Desktop.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private readonly ISessionService _sessionService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _emailOrganization = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _nameUser = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _password = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotPasswordVisible))]
    private bool _isPasswordVisible = false;

    public bool IsNotPasswordVisible => !IsPasswordVisible;

    [ObservableProperty]
    private string _passwordIconKind = "Eye";

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public LoginViewModel(IAuthService authService, ISessionService sessionService)
    {
        _authService = authService;
        _sessionService = sessionService;
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        IsBusy = true;
        ErrorMessage = string.Empty;

        LoginCommand.NotifyCanExecuteChanged();

        var request = new LoginRequestDto
        {
            EmailOrganization = this.EmailOrganization,
            NameUser = this.NameUser,
            Password = this.Password
        };

        var response = await _authService.LoginAsync(request);

        if (response.Success)
        {
            _sessionService.SetSession(response.UserId, response.OrganizationId, response.Token ?? string.Empty, this.NameUser);

            ErrorMessage = "¡Login exitoso! Redirigiendo...";

            System.Windows.Window? mainView = null;

            if (response.RoleName?.ToUpper() == "AIRPORTADMIN")
            {
                mainView = App.AppHost?.Services.GetRequiredService<AirportAdminMainView>();
            }
            else if (response.RoleName?.ToUpper() == "OPERATIONAIRPORT")
            {
                mainView = App.AppHost?.Services.GetRequiredService<OperationalUserMainView>();
            }
            else
            {
                mainView = App.AppHost?.Services.GetRequiredService<SuperAdminMainView>();
            }

            if (mainView != null)
            {
                mainView.Show();
                System.Windows.Application.Current.MainWindow?.Close();
            }
        }
        else
        {
            ErrorMessage = response.ErrorMessage!;
            Password = string.Empty;
        }

        IsBusy = false;
        LoginCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private void TogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
        PasswordIconKind = IsPasswordVisible ? "EyeOff" : "Eye";
    }

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(EmailOrganization) 
            && System.Text.RegularExpressions.Regex.IsMatch(EmailOrganization, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            && !string.IsNullOrWhiteSpace(NameUser)
            && !string.IsNullOrWhiteSpace(Password)
            && !IsBusy; 
    }
}
