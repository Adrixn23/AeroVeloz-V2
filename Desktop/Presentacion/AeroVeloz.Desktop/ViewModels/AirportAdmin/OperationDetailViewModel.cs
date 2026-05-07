
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AeroVeloz.Desktop.Models.DTOs.Operation;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public class ChangeTypeModel
{
    public short Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public partial class OperationDetailViewModel : BaseViewModel
{
    private readonly IOperationService _operationService;
    private readonly IDialogService _dialogService;
    private readonly IAirportService _airportService;
    private bool _isEditMode;
    private Guid _currentOperationId;

    public ObservableCollection<ChangeTypeModel> OperationalTypes { get; } = new ObservableCollection<ChangeTypeModel>
    {
        new ChangeTypeModel { Id = 1, Name = "Cambio de Puerta (GATE_CHANGE)" },
        new ChangeTypeModel { Id = 2, Name = "Retraso de Vuelo (FLIGHT_DELAY)" },
        new ChangeTypeModel { Id = 3, Name = "Cancelación de Vuelo (FLIGHT_CANCELLATION)" }
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
    private string title = "Crear Cambio Operacional";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [Range(1, short.MaxValue, ErrorMessage = "Debe ser mayor a 0.")]
    private short idOperationalType;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [Range(1, short.MaxValue, ErrorMessage = "Vuelo inválido.")]
    private short flightNumber;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Debe tener 3 cc.")]
    private string codeAirline = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "Debe tener 4 cc.")]
    private string codeAirport = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(25, ErrorMessage = "Máx 25 caracteres.")]
    private string previousValue = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
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

    [ObservableProperty]
    private bool isAirportIcaoEditable = true;

    public Action? OnSavedResultAction { get; set; }

    public OperationDetailViewModel(
        IOperationService operationService,
        IDialogService dialogService,
        IAirportService airportService)
    {
        _operationService = operationService;
        _dialogService = dialogService;
        _airportService = airportService;
    }

    public async void InitializeForCreate()
    {
        _isEditMode = false;
        Title = "Crear Cambio Operacional";
        IdOperationalType = 0;
        FlightNumber = 0;
        CodeAirline = string.Empty;
        CodeAirport = string.Empty;
        PreviousValue = string.Empty;
        NewValue = string.Empty;
        Cause = string.Empty;
        IsAirportIcaoEditable = true;

      
        try
        {
            var airport = await _airportService.GetByIdAsync(-1);
            if (airport != null)
            {
                CodeAirport = airport.CodeAirportIcao ?? string.Empty;
            }
        }
        catch
        {
        }
    }

    public void InitializeForEdit(OperationDto operation)
    {
        _isEditMode = true;
        Title = $"Editar Cambio Operacional: {operation.FlightNumber}";
        if (Guid.TryParse(operation.Id, out var id))
        {
            _currentOperationId = id;
        }
        IdOperationalType = operation.IdOperationalType;
        FlightNumber = operation.FlightNumber;
        CodeAirline = operation.CodeAirline ?? string.Empty;
        CodeAirport = operation.CodeAirport ?? string.Empty;
        PreviousValue = operation.PreviousValue ?? string.Empty;
        NewValue = operation.NewValue ?? string.Empty;
        Cause = operation.Cause ?? string.Empty;
        IsAirportIcaoEditable = false;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            var firstError = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
            await _dialogService.ShowInfoAsync($"Por favor corrija los siguientes errores:\n{firstError}", "Validación");
            return;
        }

        string finalCause = IsCustomCause ? CustomCause : Cause;
        if (string.IsNullOrWhiteSpace(finalCause))
        {
            await _dialogService.ShowInfoAsync("Debe indicar una causa válida.", "Validación");
            return;
        }

        IsBusy = true;
        bool result = false;

        try
        {
            var operationData = new
            {
                IdOperationalType,
                FlightNumber,
                CodeAirline,
                CodeAirport,
                PreviousValue,
                NewValue,
                Cause = finalCause
            };

            if (_isEditMode)
            {
                result = await _operationService.UpdateOperationAsync(_currentOperationId, operationData);
            }
            else
            {
                result = await _operationService.CreateOperationAsync(operationData);
            }

            IsBusy = false;

            if (result)
            {
                await _dialogService.ShowInfoAsync("Cambio operacional guardado exitosamente.");
                DialogHost.CloseDialogCommand.Execute(null, null);
                OnSavedResultAction?.Invoke();
            }
            else
            {
                await _dialogService.ShowInfoAsync("Error al guardar el cambio operacional. Intente nuevamente.", "Error");
            }
        }
        catch (Exception)
        {
            IsBusy = false;
            await _dialogService.ShowErrorAsync("Error", "Ocurrió un error inesperado al procesar la operación. Intente nuevamente.");
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        DialogHost.CloseDialogCommand.Execute(null, null);
    }
}

