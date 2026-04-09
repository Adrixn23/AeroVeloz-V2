
using AeroVeloz.Desktop.Models.DTOs.User;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.Services.Interfaces.Users;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class UserDetailViewModel : BaseViewModel
{
    private readonly IManagerUserService _managerUserService;
    private readonly IDialogService _dialogService;
    private readonly ISessionService _sessionService;
    private bool _isEditMode;
    private Guid _currentUserId;

    private const short OPERATION_AIRPORT_ROLE_ID = 4; 

    [ObservableProperty]
    private string title = "Crear Operador";

    [ObservableProperty]
    private string userName = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool isActive = true;

    public Action? OnSavedResultAction { get; set; }

    public UserDetailViewModel(
        IManagerUserService managerUserService,
        IDialogService dialogService,
        ISessionService sessionService)
    {
        _managerUserService = managerUserService;
        _dialogService = dialogService;
        _sessionService = sessionService;
    }

    public void InitializeForCreate()
    {
        _isEditMode = false;
        Title = "Crear Nuevo Operador";
        UserName = string.Empty;
        Password = string.Empty;
        IsActive = true;
    }

    public void InitializeForEdit(UserDto user)
    {
        _isEditMode = true;
        Title = $"Editar Operador: {user.FullName}";
        if (Guid.TryParse(user.Id, out var id))
        {
            _currentUserId = id;
        }
        UserName = user.FullName ?? string.Empty;
        Password = string.Empty; 
        IsActive = user.IsActive;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            await _dialogService.ShowInfoAsync("Debe ingresar el nombre del usuario.", "Validación");
            return;
        }

        IsBusy = true;
        bool result;

        try
        {
            if (_isEditMode)
            {
                var dto = new EditUserDto
                {
                    IdUser = _currentUserId,
                    NameUser = UserName,
                    Password = string.IsNullOrWhiteSpace(Password) ? null : Password,
                    IsActive = IsActive,
                    IdRol = OPERATION_AIRPORT_ROLE_ID,
                    IdOrganization = _sessionService.OrgId
                };
                result = await _managerUserService.UpdateUserAsync(_currentUserId, dto);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    IsBusy = false;
                    await _dialogService.ShowInfoAsync("Debe ingresar contraseña para el nuevo usuario.", "Validación");
                    return;
                }

                var dto = new CreateUserDto
                {
                    UserName = UserName,
                    Password = Password,
                    IdOrganization = _sessionService.OrgId,
                    IdRol = OPERATION_AIRPORT_ROLE_ID
                };
                result = await _managerUserService.CreateUserAsync(dto);
            }

            IsBusy = false;

            if (result)
            {
                await _dialogService.ShowInfoAsync("Usuario guardado exitosamente.");
                DialogHost.CloseDialogCommand.Execute(null, null);
                OnSavedResultAction?.Invoke();
            }
            else
            {
                await _dialogService.ShowInfoAsync("Error al guardar el usuario. Intente nuevamente.", "Error");
            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await _dialogService.ShowErrorAsync("Error", $"Ocurrió un error: {ex.Message}");
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        DialogHost.CloseDialogCommand.Execute(null, null);
    }
}



