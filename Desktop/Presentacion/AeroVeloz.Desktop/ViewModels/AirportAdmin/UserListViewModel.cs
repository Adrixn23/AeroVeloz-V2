using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.User;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Users;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class UserListViewModel : BaseViewModel
{
    private readonly IManagerUserService _managerUserService;
    private readonly IDialogService _dialogService;
    private readonly UserDetailViewModel _detailViewModel;
    private readonly AuditLogViewModel _auditViewModel;

    [ObservableProperty]
    private ObservableCollection<UserDto> _users = new();

    [ObservableProperty]
    private UserDto? _selectedUser;

    [ObservableProperty]
    private string _searchText = string.Empty;

    private ObservableCollection<UserDto> _allUsers = new();

    public UserListViewModel(
        IManagerUserService managerUserService,
        IDialogService dialogService,
        UserDetailViewModel detailViewModel,
        AuditLogViewModel auditViewModel)
    {
        _managerUserService = managerUserService;
        _dialogService = dialogService;
        _detailViewModel = detailViewModel;
        _auditViewModel = auditViewModel;

        _detailViewModel.OnSavedResultAction += async () => await LoadUsersAsync();
    }

    partial void OnSearchTextChanged(string? value)
    {
        FilterUsers();
    }

    private void FilterUsers()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Users = new ObservableCollection<UserDto>(_allUsers);
        }
        else
        {
            var filtered = _allUsers
                .Where(u => u.FullName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || u.Email?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            Users = new ObservableCollection<UserDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task LoadUsersAsync()
    {
        IsBusy = true;
        try
        {
            var users = await _managerUserService.GetAirportUsersAsync();
            _allUsers = new ObservableCollection<UserDto>(users);
            FilterUsers();
        }
        catch
        {
            await _dialogService.ShowErrorAsync("Error", "No se pudieron cargar los operadores.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CreateUserAsync()
    {
        _detailViewModel.InitializeForCreate();
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task ViewDetailsAsync(UserDto? user)
    {
        var userToView = user ?? SelectedUser;
        if (userToView == null) return;

        string details = $"ID: {userToView.Id}\n" +
                         $"Nombre: {userToView.FullName}\n" +
                         $"Email: {userToView.Email}\n" +
                         $"Activo: {(userToView.IsActive ? "Sí" : "No")}";

        await _dialogService.ShowInfoAsync(details, "Detalles del Operador");
    }

    [RelayCommand]
    private async Task EditUserAsync()
    {
        if (SelectedUser == null) return;

        _detailViewModel.InitializeForEdit(SelectedUser);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task DeleteUserAsync()
    {
        if (SelectedUser == null) return;

        var confirm = await _dialogService.ShowConfirmationAsync(
            $"¿Desactivar a {SelectedUser.FullName}? Esta acción se registrará en auditoría.",
            "Confirmar desactivación");

        if (!confirm) return;

        IsBusy = true;
        try
        {
            if (Guid.TryParse(SelectedUser.Id, out var userId))
            {
                var result = await _managerUserService.DeleteUserAsync(userId);
                if (result)
                {
                    await _dialogService.ShowInfoAsync("Operador desactivado exitosamente.");
                    await LoadUsersAsync();
                }
                else
                {
                    await _dialogService.ShowErrorAsync("Error", "No se pudo desactivar el operador.");
                }
            }
        }
        catch (Exception)
        {
            await _dialogService.ShowErrorAsync("Error", "Ocurrió un error inesperado al procesar la operación. Intente nuevamente.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ViewUserAuditAsync()
    {
        if (SelectedUser == null) return;

        if (Guid.TryParse(SelectedUser.Id, out var parsedId))
        {
            _auditViewModel.TargetUserId = parsedId;
        }
        _auditViewModel.AuditMode = "User Specific";

        // Initialize and show modal
        var auditView = new Views.AirportAdmin.AuditLogView { DataContext = _auditViewModel };
        _auditViewModel.LoadAuditLogsCommand.Execute(null);
        await DialogHost.Show(auditView, "RootDialog");
    }
}



