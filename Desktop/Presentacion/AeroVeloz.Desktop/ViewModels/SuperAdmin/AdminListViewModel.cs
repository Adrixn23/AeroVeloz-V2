using System.Collections.ObjectModel;

using AeroVeloz.Desktop.Models.DTOs.Audit;
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AuthUserDto = AeroVeloz.Desktop.Models.DTOs.Auth.UserDto;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using AeroVeloz.Desktop.Services.Interfaces.AdminSystem;
using AeroVeloz.Desktop.Services.Interfaces.Audit;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class AdminListViewModel : BaseViewModel
{
    private readonly IAdminManagerService _adminManagerService;
    private readonly IAuditService _auditService;
    private readonly IDialogService _dialogService;
    private readonly AeroVeloz.Desktop.Services.Interfaces.Auth.ISessionService _sessionService;
    private readonly AdminDetailViewModel _detailViewModel;
    private readonly AuditLogViewModel _auditViewModel;

    [ObservableProperty]
    private ObservableCollection<AuthUserDto> _systemUsers = new();

    private ObservableCollection<AuthUserDto> _allSystemUsers = new();

    [ObservableProperty]
    private AuthUserDto? _selectedUser;

    [ObservableProperty]
    private string _searchText = string.Empty;

    

    [ObservableProperty]
    private string _auditTitle = "Registro de auditorÃ­a";

    

    partial void OnSearchTextChanged(string? value)
    {
        FilterUsers();
    }

    public AdminListViewModel(
        IAdminManagerService adminManagerService,
        IAuditService auditService,
        IDialogService dialogService,
        AeroVeloz.Desktop.Services.Interfaces.Auth.ISessionService sessionService,
        AdminDetailViewModel detailViewModel,
        AuditLogViewModel auditViewModel)
    {
        _adminManagerService = adminManagerService;
        _auditService = auditService;
        _dialogService = dialogService;
        _sessionService = sessionService;
        _detailViewModel = detailViewModel;
        _auditViewModel = auditViewModel;

        _detailViewModel.OnSavedResultAction = async () => await LoadDataAsync();
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        IsBusy = true;

        var users = await _adminManagerService.GetAvailableAdminsAsync();

        _allSystemUsers.Clear();
        foreach (var user in users)
        {
            _allSystemUsers.Add(user);
        }

        FilterUsers();
        IsBusy = false;
    }

    private void FilterUsers()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            SystemUsers = new ObservableCollection<UserDto>(_allSystemUsers);
        }
        else
        {
            var filtered = _allSystemUsers.Where(u => 
                (u.FullName?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                (u.Email?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false));

            SystemUsers = new ObservableCollection<UserDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task ViewDetailsAsync(AuthUserDto? user)
    {
        var userToView = user ?? SelectedUser;
        if (userToView == null) return;

        string details = $"Nombre: {userToView.FullName}\n" +
                         $"Email: {userToView.Email}\n" +
                         $"Rol: {userToView.Role}\n" +
                         $"Activo: {(userToView.IsActive ? "SÃ­" : "No")}\n" +
                         $"Bloqueado: {(userToView.IsBlocked ? "SÃ­" : "No")}";

        await _dialogService.ShowInfoAsync(details, "Detalles del Usuario");
    }

    [RelayCommand]
    private async Task ViewUserAuditAsync(AuthUserDto? user)
    {
        var userToAudit = user ?? SelectedUser;
        if (userToAudit == null) return;

        if (Guid.TryParse(userToAudit.Id, out var parsedId))
        {
            _auditViewModel.TargetUserId = parsedId;
        }
        _auditViewModel.AuditMode = "User Specific";

        var auditView = new Views.AirportAdmin.AuditLogView { DataContext = _auditViewModel };
        _auditViewModel.LoadAuditLogsCommand?.Execute(null);
        await DialogHost.Show(auditView, "RootDialog");
    }

    [RelayCommand]
    private async Task CreateUserAsync()
    {
        _detailViewModel.InitializeForCreate();
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task EditUserAsync(AuthUserDto? user)
    {
        var userToEdit = user ?? SelectedUser;
        if (userToEdit == null) return;

        _detailViewModel.InitializeForEdit(userToEdit);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task DeactivateUserAsync(AuthUserDto? user)
    {
        var userToDeactivate = user ?? SelectedUser;
        if (userToDeactivate == null) return;

        var confirm = await _dialogService.ShowConfirmationAsync(
            $"Â¿Desactivas a {userToDeactivate.FullName}? Esta acciÃ³n se registrarÃ¡ en auditorÃ­a.",
            "Confirmar desactivaciÃ³n");

        if (!confirm) return;

        IsBusy = true;

        if (!Guid.TryParse(userToDeactivate.Id, out var userId))
        {
            IsBusy = false;
            return;
        }

        var success = await _adminManagerService.DeactivateUserAsync(userId);
        IsBusy = false;

        if (success)
        {
            await _dialogService.ShowInfoAsync("Usuario desactivado exitosamente.", "DesactivaciÃ³n Exitosa");
            SelectedUser = null;
            await LoadDataAsync();
        }
        else
        {
            await _dialogService.ShowInfoAsync("Error al desactivar el usuario. Intenta nuevamente.", "Error");
        }
    }
}



