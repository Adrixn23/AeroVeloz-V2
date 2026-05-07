using AeroVeloz.Desktop.Models.DTOs.StatusSystem;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.OperationalUser;

public partial class OperationalUserDashboardViewModel : BaseViewModel
{
    private readonly IAirportAdminStatService _statService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool isLoading = true;

    [ObservableProperty]
    private AirportAdminStatsDto? statistics;

    public Action<string>? OnNavigate { get; set; }

    [RelayCommand]
    private void NavigateTo(string destination)
    {
        OnNavigate?.Invoke(destination);
    }

    public bool IsNotLoading => !IsLoading;

    public OperationalUserDashboardViewModel(IAirportAdminStatService statService)
    {
        _statService = statService;
        LoadStatistics();
    }

    private async void LoadStatistics()
    {
        try
        {
            IsLoading = true;
            var stats = await _statService.GetAirportStatsAsync();
            if (stats != null)
            {
                Statistics = stats;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading statistics: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
