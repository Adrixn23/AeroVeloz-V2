using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Audit;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Audit;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class AuditLogViewModel : BaseViewModel
{
    private readonly IAuditService _auditService;
    private readonly IDialogService _dialogService;
    private readonly ISessionService _sessionService;

    public Guid? TargetUserId { get; set; } = null;

    [ObservableProperty]
    private ObservableCollection<AuditDto> auditLogs = new();

    [ObservableProperty]
    private AuditDto? selectedAudit;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private DateTime filterStartDate = DateTime.Today.AddDays(-30);

    [ObservableProperty]
    private DateTime filterEndDate = DateTime.Today;

    [ObservableProperty]
    private string auditMode = "Global"; 

    private ObservableCollection<AuditDto> _allAuditLogs = new();

    public AuditLogViewModel(
        IAuditService auditService,
        IDialogService dialogService,
        ISessionService sessionService)
    {
        _auditService = auditService;
        _dialogService = dialogService;
        _sessionService = sessionService;
    }

    [ObservableProperty]
    private string filterUser = string.Empty;

    partial void OnSearchTextChanged(string? value)
    {
        FilterLogs();
    }

    partial void OnFilterUserChanged(string? value)
    {
        FilterLogs();
    }

    private void FilterLogs()
    {
        var filtered = _allAuditLogs
            .Where(a => a.OccurredAt >= FilterStartDate && a.OccurredAt <= FilterEndDate);

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(a => 
                a.AuditTypeName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                || a.NameUser?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                || a.NameEntity?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true);
        }

        if (!string.IsNullOrWhiteSpace(FilterUser))
        {
            filtered = filtered.Where(a => a.NameUser?.Contains(FilterUser, StringComparison.OrdinalIgnoreCase) == true);
        }

        AuditLogs = new ObservableCollection<AuditDto>(filtered);
    }

    [RelayCommand]
    private async Task LoadAuditLogsAsync()
    {
        IsBusy = true;
        try
        {
            if (AuditMode == "Global")
            {
                var logs = await _auditService.GetGlobalAuditAsync(_sessionService.OrgId);
                _allAuditLogs = new ObservableCollection<AuditDto>(logs);
            }
            else
            {
                var userId = TargetUserId ?? _sessionService.UserId;
                var logs = await _auditService.GetUserAuditAsync(userId);
                _allAuditLogs = new ObservableCollection<AuditDto>(logs);
            }

            FilterLogs();
        }
        catch (Exception)
        {
            await _dialogService.ShowErrorAsync("Error", "No se pudieron cargar los registros de auditoría. Si el problema persiste, contacte a soporte técnico.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ApplyFiltersAsync()
    {
        await LoadAuditLogsAsync();
    }

    [RelayCommand]
    private async Task ExportLogsAsync()
    {
        
        await _dialogService.ShowInfoAsync("Exportar auditoría - Funcionalidad en desarrollo", "Información");
    }

    [RelayCommand]
    private void SwitchAuditMode()
    {
        AuditMode = AuditMode == "Global" ? "User Specific" : "Global";
      
        LoadAuditLogsCommand.Execute(null);
    }
}




