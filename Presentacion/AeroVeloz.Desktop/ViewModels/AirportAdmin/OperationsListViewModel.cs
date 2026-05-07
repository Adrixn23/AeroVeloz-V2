using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Operation;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class OperationsListViewModel : BaseViewModel
{
    private readonly IOperationService _operationService;
    private readonly IDialogService _dialogService;
    private readonly OperationDetailViewModel _detailViewModel;

    [ObservableProperty]
    private ObservableCollection<OperationDto> _operations = new();

    [ObservableProperty]
    private OperationDto? _selectedOperation;

    [ObservableProperty]
    private string _searchText = string.Empty;

    private ObservableCollection<OperationDto> _allOperations = new();

    public Action? OnSavedResultAction { get; set; }

    public OperationsListViewModel(
        IOperationService operationService,
        IDialogService dialogService,
        OperationDetailViewModel detailViewModel)
    {
        _operationService = operationService;
        _dialogService = dialogService;
        _detailViewModel = detailViewModel;

        _detailViewModel.OnSavedResultAction += async () => await LoadOperationsAsync();
    }

    partial void OnSearchTextChanged(string? value)
    {
        FilterOperations();
    }

    private void FilterOperations()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Operations = new ObservableCollection<OperationDto>(_allOperations);
        }
        else
        {
            var filtered = _allOperations
                .Where(o => o.CodeAirline?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || o.CodeAirport?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true
                         || o.Cause?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            Operations = new ObservableCollection<OperationDto>(filtered);
        }
    }

    [RelayCommand]
    private async Task LoadOperationsAsync()
    {
        IsBusy = true;
        try
        {
            var operations = await _operationService.GetAirportOperationsAsync();
            _allOperations = new ObservableCollection<OperationDto>(operations);
            FilterOperations();
        }
        catch
        {
            await _dialogService.ShowErrorAsync("Error", "No se pudieron cargar las operaciones.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CreateOperationAsync()
    {
        _detailViewModel.InitializeForCreate();
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task EditOperationAsync(OperationDto? operation = null)
    {
        var target = operation ?? SelectedOperation;
        if (target == null) return;

        _detailViewModel.InitializeForEdit(target);
        await DialogHost.Show(_detailViewModel, "RootDialog");
    }

    [RelayCommand]
    private async Task OperarVueloAsync()
    {
        // En una futura versión esto abrirá la lista real de vuelos disponibles en el aeropuerto
        await _dialogService.ShowInfoAsync("La selección y gestión directa de vuelos a tiempo real estará disponible tras implementar el catálogo en los servicios del aeropuerto.", "Vuelos");
    }

    [RelayCommand]
    private async Task DeleteOperationAsync(OperationDto? operation = null)
    {
        var target = operation ?? SelectedOperation;
        if (target == null) return;

        var confirm = await _dialogService.ShowConfirmationAsync(
            $"¿Eliminar la operación {target.FlightNumber}?",
            "Confirmar eliminación");

        if (!confirm) return;

        IsBusy = true;
        try
        {
            if (Guid.TryParse(target.Id, out var operationId))
            {
                var result = await _operationService.DeleteOperationAsync(operationId);
                if (result)
                {
                    await _dialogService.ShowInfoAsync("Operación eliminada exitosamente.");
                    await LoadOperationsAsync();
                    OnSavedResultAction?.Invoke();
                }
                else
                {
                    await _dialogService.ShowErrorAsync("Error", "No se pudo eliminar la operación.");
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
    private async Task ViewDetailsAsync(OperationDto operation)
    {
        var op = operation ?? SelectedOperation;
        if (op == null) return;

        string details = "ID: " + op.Id + "\n" +
                         "Tipo: " + op.IdOperationalType + "\n" +
                         "Vuelo: " + op.FlightNumber + "\n" +
                         "Aerolínea: " + op.CodeAirline + "\n" +
                         "Aeropuerto: " + op.CodeAirport + "\n" +
                         "Causa: " + op.Cause;

        await _dialogService.ShowInfoAsync(details, "Detalles de Operación");
    }
}