using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Organization;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Organization;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class AirlineListViewModel : BaseViewModel
{
    private readonly IOrganizationService _organizationService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private ObservableCollection<OrganizationDto> _airlines = new();

    private ObservableCollection<OrganizationDto> _allAirlines = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        FilterAirlines();
    }

    public AirlineListViewModel(IOrganizationService organizationService, IDialogService dialogService)
    {
        _organizationService = organizationService;
        _dialogService = dialogService;
    }

    [RelayCommand]
    public async Task LoadAirlinesAsync()
    {
        IsBusy = true;
        var result = await _organizationService.GetOrganizationsByTypeAsync("AIRLINE");

        _allAirlines.Clear();
        foreach (var item in result)
        {
            _allAirlines.Add(item);
        }

        FilterAirlines();
        IsBusy = false;
    }

    private void FilterAirlines()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Airlines = new ObservableCollection<OrganizationDto>(_allAirlines);
        }
        else
        {
            var filtered = _allAirlines.Where(a => 
                (a.NameOrganization?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (a.EmailOrganization?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));

            Airlines = new ObservableCollection<OrganizationDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task BlockAirlineAsync(OrganizationDto selectedAirline)
    {
        if (selectedAirline == null) return;

        var contentDialog = $"¿Estás seguro que deseas bloquear la aerolínea '{selectedAirline.NameOrganization}'?";
        var confirm = await _dialogService.ShowConfirmationAsync(contentDialog, "Bloquear Aerolínea");

        if (confirm)
        {
            IsBusy = true;
            var result = await _organizationService.BlockOrganizationAsync(selectedAirline.Id);
            IsBusy = false;

            if (result)
            {
                await _dialogService.ShowInfoAsync("Aerolínea bloqueada exitosamente.");
                await LoadAirlinesAsync();
            }
            else
            {
                await _dialogService.ShowErrorAsync("No se pudo bloquear la aerolínea.");
            }
        }
    }

    [RelayCommand]
    private async Task RefreshAirlinesAsync()
    {
        await LoadAirlinesAsync();
    }

    [RelayCommand]
    private async Task ViewDetailsAsync(OrganizationDto selectedAirline)
    {
        if (selectedAirline == null) return;

        string details = $"Aerolínea: {selectedAirline.NameOrganization}\n" +
                         $"Correo: {selectedAirline.EmailOrganization}\n" +
                         $"Tipo: {selectedAirline.TypeOrganization}\n" +
                         $"Activa: {(selectedAirline.IsActived ? "Sí" : "No")}";

        await _dialogService.ShowInfoAsync(details, "Detalles de la Aerolínea");
    }
}
