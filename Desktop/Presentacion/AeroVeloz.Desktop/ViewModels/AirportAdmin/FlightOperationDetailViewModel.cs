using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AeroVeloz.Desktop.Models.DTOs.Flight;
using AeroVeloz.Desktop.Models.DTOs.Operation;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class FlightOperationDetailViewModel : BaseViewModel
{
    private readonly IOperationService _operationService;
    private readonly IFlightService _flightService;
    private readonly IDialogService _dialogService;

    private FlightForOperationDto? _currentFlight;
    private bool _isCreatingNewOperation;

    public ObservableCollection<OperationTypeModel> OperationalTypes { get; } = new ObservableCollection<OperationTypeModel>
    {
        new OperationTypeModel { Id = 1, Name = "Cambio de Puerta (GATE_CHANGE)" },
        new OperationTypeModel { Id = 2, Name = "Retraso de Vuelo (FLIGHT_DELAY)" },
        new OperationTypeModel { Id = 3, Name = "Cancelación de Vuelo (FLIGHT_CANCELLATION)" }
    };

    public ObservableCollection<string> CausesList { get; } = new ObservableCollection<string>
    {
        "Condiciones climáticas",
        "Problemas técnicos",
        "Tráfico aéreo",
        "Razones operativas",
        "Mantenimiento no programado",
        "Huelga",
        "Otros"
    };

    [ObservableProperty]
    private string title = "Operación del Vuelo";

    [ObservableProperty]
    private string flightNumber = string.Empty;

    [ObservableProperty]
    private string flightInfo = string.Empty;

    [ObservableProperty]
    private ObservableCollection<FlightOperationDto> flightOperations = new();

    [ObservableProperty]
    private FlightOperationDto? selectedOperation;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [Range(1, short.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    private short idOperationalType;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [MaxLength(25, ErrorMessage = "Máx 25 caracteres.")]
    private string previousValue = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [MaxLength(25, ErrorMessage = "Máx 25 caracteres.")]
    private string newValue = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [MaxLength(250, ErrorMessage = "Máx 250 caracteres.")]
    private string cause = string.Empty;

    [ObservableProperty]
    private bool isCustomCause = false;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(250, ErrorMessage = "Máx 250 caracteres.")]
    private string customCause = string.Empty;

    partial void OnCauseChanged(string? value)
    {
        IsCustomCause = value == "Otros";
        if (!IsCustomCause)
        {
            CustomCause = string.Empty;
        }
    }

    public Action? OnSavedResultAction { get; set; }

    public FlightOperationDetailViewModel(
        IOperationService operationService,
        IFlightService flightService,
        IDialogService dialogService)
    {
        _operationService = operationService;
        _flightService = flightService;
        _dialogService = dialogService;
    }

    public void InitializeForFlightOperation(FlightForOperationDto flight)
    {
        _currentFlight = flight;
        _isCreatingNewOperation = true;

        Title = $"Nueva Operación - Vuelo {flight.FlightNumber}";
        FlightNumber = flight.FlightNumber ?? string.Empty;
        FlightInfo = $"{flight.OriginAirport} → {flight.DestinationAirport} | {flight.FlightStateName}";

        LoadFlightOperationsAsync();

        ResetFormFields();
    }

    private async void LoadFlightOperationsAsync()
    {
        if (_currentFlight == null) return;

        try
        {
            var operations = await _flightService.GetFlightOperationsAsync(_currentFlight.Id);
            FlightOperations = new ObservableCollection<FlightOperationDto>(operations.Where(op => op.IsActive));
        }
        catch
        {
            await _dialogService.ShowErrorAsync("Error", "No se pudieron cargar las operaciones del vuelo");
        }
    }

    private void ResetFormFields()
    {
        IdOperationalType = 0;
        PreviousValue = string.Empty;
        NewValue = string.Empty;
        Cause = string.Empty;
        CustomCause = string.Empty;
        SelectedOperation = null;
    }

    [RelayCommand]
    private async Task SaveOperationAsync()
    {
        if (_currentFlight == null)
        {
            await _dialogService.ShowErrorAsync("Error", "No hay vuelo seleccionado");
            return;
        }

        ValidateAllProperties();
        if (HasErrors)
        {
            await _dialogService.ShowErrorAsync("Validación", "Por favor completa todos los campos requeridos");
            return;
        }

        IsBusy = true;
        try
        {
            var finalCause = IsCustomCause ? CustomCause : Cause;

            var operationDto = new OperationDto
            {
                IdOperationalType = IdOperationalType,
                FlightNumber = _currentFlight.Id,
                CodeAirline = _currentFlight.CodeAirlineIcao,
                CodeAirport = "XXX",
                PreviousValue = PreviousValue,
                NewValue = NewValue,
                Cause = finalCause,
                IsActive = true
            };

            var success = await _operationService.CreateOperationAsync(operationDto);

            if (success)
            {
                await _dialogService.ShowErrorAsync("Éxito", "Operación registrada correctamente");
                OnSavedResultAction?.Invoke();
                ResetFormFields();
            }
            else
            {
                await _dialogService.ShowErrorAsync("Error", "No se pudo registrar la operación");
            }
        }
        catch (Exception ex)
        {
            await _dialogService.ShowErrorAsync("Error", $"Error al registrar la operación: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteOperationAsync(FlightOperationDto? operation = null)
    {
        var target = operation ?? SelectedOperation;
        if (target == null) return;

        // Usar ShowErrorAsync como confirmación (para mantener consistencia con el patrón)
        await _dialogService.ShowErrorAsync(
            "Confirmar",
            "¿Deseas desactivar esta operación?");

        IsBusy = true;
        try
        {
            var success = await _operationService.DeleteOperationAsync(target.Id);

            if (success)
            {
                await _dialogService.ShowErrorAsync("Éxito", "Operación desactivada correctamente");
                LoadFlightOperationsAsync();
            }
            else
            {
                await _dialogService.ShowErrorAsync("Error", "No se pudo desactivar la operación");
            }
        }
        catch (Exception ex)
        {
            await _dialogService.ShowErrorAsync("Error", $"Error al desactivar la operación: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        ResetFormFields();
        await Task.CompletedTask;
    }
}

public class OperationTypeModel
{
    public short Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
