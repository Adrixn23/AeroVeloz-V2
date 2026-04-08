using System.Collections.ObjectModel;

using AeroVeloz.Desktop.Models.DTOs.Audit;
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AeroVeloz.Desktop.Models.DTOs.User;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class AdminListViewModel : BaseViewModel
{
    private readonly IAdminManagerService _adminManagerService;
    private readonly IAuditService _auditService;
    private readonly IDialogService _dialogService;
    private readonly AdminDetailViewModel _detailViewModel;

    [ObservableProperty]
    private ObservableCollection<UserDto> _systemUsers = new();

    private ObservableCollection<UserDto> _allSystemUsers = new();

    [ObservableProperty]
    private UserDto? _selectedUser;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private ObservableCollection<AuditDto> _selectedUserAudit = new();

    [ObservableProperty]
    private bool _isAuditVisible = false;

    partial void OnSearchTextChanged(string? value)
    {
        FilterUsers();
    }

    partial void OnSelectedUserChanged(UserDto? value)
    {
        if (value != null)
        {
            LoadUserAuditAsync();
        }
        else
        {
            SelectedUserAudit.Clear();
            IsAuditVisible = false;
        }
    }

    public AdminListViewModel(
        IAdminManagerService adminManagerService,
        IAuditService auditService,
        IDialogService dialogService,
        AdminDetailViewModel detailViewModel)
    {
        _adminManagerService = adminManagerService;
        _auditService = auditService;
        _dialogService = dialogService;
        _detailViewModel = detailViewModel;

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

    private async Task LoadUserAuditAsync()
    {
        if (SelectedUser == null) return;

        IsBusy = true;

        if (!Guid.TryParse(SelectedUser.Id, out var userId))
        {
            IsBusy = false;
            return;
        }

        var audits = await _auditService.GetUserAuditAsync(userId);

        SelectedUserAudit.Clear();
        foreach (var audit in audits.OrderByDescending(a => a.OccurredAt))
        {
            SelectedUserAudit.Add(audit);
        }

        IsAuditVisible = SelectedUserAudit.Count > 0;
        IsBusy = false;
    }

    [RelayCommand]
    private async Task CreateUserAsync()
    {
        _detailViewModel.InitializeForCreate();
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task EditUserAsync(UserDto? user)
    {
        var userToEdit = user ?? SelectedUser;
        if (userToEdit == null) return;

        _detailViewModel.InitializeForEdit(userToEdit);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task DeactivateUserAsync(UserDto? user)
    {
        var userToDeactivate = user ?? SelectedUser;
        if (userToDeactivate == null) return;

        var confirm = await _dialogService.ShowConfirmationAsync(
            $"¿Desactivas a {userToDeactivate.FullName}? Esta acción se registrará en auditoría.",
            "Confirmar desactivación");

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
            await _dialogService.ShowInfoAsync("Usuario desactivado exitosamente.", "Desactivación Exitosa");
            SelectedUser = null;
            await LoadDataAsync();
        }
        else
        {
            await _dialogService.ShowInfoAsync("Error al desactivar el usuario. Intenta nuevamente.", "Error");
        }
    }
}


