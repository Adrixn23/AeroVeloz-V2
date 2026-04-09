
using AeroVeloz.Desktop.Models.DTOs.Operation;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.AirportAdmin;

public partial class OperationDetailViewModel : BaseViewModel
{
    private readonly IOperationService _operationService;
    private readonly IDialogService _dialogService;
    private bool _isEditMode;
    private Guid _currentOperationId;

    [ObservableProperty]
    private string title = "Crear Cambio Operacional";

    [ObservableProperty]
    private short idOperationalType;

    [ObservableProperty]
    private short flightNumber;

    [ObservableProperty]
    private string codeAirline = string.Empty;

    [ObservableProperty]
    private string codeAirport = string.Empty;

    [ObservableProperty]
    private string previousValue = string.Empty;

    [ObservableProperty]
    private string newValue = string.Empty;

    [ObservableProperty]
    private string cause = string.Empty;

    public Action? OnSavedResultAction { get; set; }

    public OperationDetailViewModel(
        IOperationService operationService,
        IDialogService dialogService)
    {
        _operationService = operationService;
        _dialogService = dialogService;
    }

    public void InitializeForCreate()
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
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (FlightNumber <= 0)
        {
            await _dialogService.ShowInfoAsync("Debe ingresar un número de vuelo válido.", "Validación");
            return;
        }

        if (IdOperationalType <= 0)
        {
            await _dialogService.ShowInfoAsync("Debe seleccionar un tipo de cambio operacional.", "Validación");
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
                Cause
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

