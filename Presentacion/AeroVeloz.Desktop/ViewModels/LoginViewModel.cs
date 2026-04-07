using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

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
    private bool _isLoading;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        IsLoading = true;
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
            ErrorMessage = "¡Login exitoso! Token recibido.";

        }
        else
        {
            ErrorMessage = response.ErrorMessage;
            Password = string.Empty;
        }

        IsLoading = false;
        LoginCommand.NotifyCanExecuteChanged();
    }

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(EmailOrganization) 
            && System.Text.RegularExpressions.Regex.IsMatch(EmailOrganization, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            && !string.IsNullOrWhiteSpace(NameUser)
            && !string.IsNullOrWhiteSpace(Password)
            && !IsLoading; 
    }
}
