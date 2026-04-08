using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using AeroVeloz.Desktop.Views.SuperAdmin;
using Microsoft.Extensions.DependencyInjection;

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
            _sessionService.SetSession(response.UserId, response.OrganizationId, response.Token ?? string.Empty);

            ErrorMessage = "¡Login exitoso! Redirigiendo...";

            var mainView = App.AppHost?.Services.GetRequiredService<SuperAdminMainView>();
            if (mainView != null)
            {
                mainView.Show();
                System.Windows.Application.Current.MainWindow?.Close();
            }
        }
        else
        {
            ErrorMessage = response.ErrorMessage;
            Password = string.Empty;
        }

        IsBusy = false;
        LoginCommand.NotifyCanExecuteChanged();
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
