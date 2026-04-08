using System;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace AeroVeloz.Desktop.ViewModels.SuperAdmin;

public partial class AirportDetailViewModel : BaseViewModel
{
    private readonly IAirportService _airportService;
    private readonly IDialogService _dialogService;
    
    public Action? OnSavedResultAction;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isEditMode;

    [ObservableProperty]
    private int _airportId;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _nameOrganization = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _emailOrganization = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _codeAirportIcao = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _codeAirportIata = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _country = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _city = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _timeOffsetInput = "+00:00"; // Simplification for UI binding

    public AirportDetailViewModel(
        IAirportService airportService, 
        IDialogService dialogService)
    {
        _airportService = airportService;
        _dialogService = dialogService;
    }

    public void InitializeForCreate()
    {
        IsEditMode = false;
        Title = "Registrar Nuevo Aeropuerto";
        AirportId = 0;
        NameOrganization = string.Empty;
        EmailOrganization = string.Empty;
        CodeAirportIcao = string.Empty;
        CodeAirportIata = string.Empty;
        Country = string.Empty;
        City = string.Empty;
        TimeOffsetInput = "+00:00";
    }

    public void InitializeForEdit(AirportDto airport)
    {
        IsEditMode = true;
        Title = "Editar Aeropuerto";
        AirportId = airport.Id;
        NameOrganization = airport.NameOrganization;
        EmailOrganization = airport.EmailOrganization;
        CodeAirportIcao = airport.CodeAirportIcao;
        CodeAirportIata = airport.CodeAirportIata;
        Country = airport.Country;
        City = airport.City;
        TimeOffsetInput = airport.TimeOffset.ToString("hh\\:mm");
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (!TimeSpan.TryParse(TimeOffsetInput.Replace("+", "").Replace("-", ""), out var offset))
        {
            await _dialogService.ShowWarningAsync("Formato de diferencia horaria incorrecto.");
            return;
        }

        IsBusy = true;
        bool success = false;

        try
        {
            if (IsEditMode)
            {
                var dto = new AirportDto
                {
                    Id = AirportId,
                    NameOrganization = NameOrganization,
                    EmailOrganization = EmailOrganization,
                    CodeAirportIcao = CodeAirportIcao,
                    CodeAirportIata = CodeAirportIata,
                    Country = Country,
                    City = City,
                    TimeOffset = new DateTimeOffset(DateTime.UtcNow.Date, TimeOffsetInput.StartsWith("-") ? -offset : offset),
                    TypeOrganization = "Airport",
                    IsActived = true, 
                    CreateAt = DateTime.UtcNow
                };

                success = await _airportService.UpdateAsync(AirportId, dto);
            }
            else
            {
                var dto = new CreateAirportDto
                {
                    NameOrganization = NameOrganization,
                    EmailOrganization = EmailOrganization,
                    CodeAirportIcao = CodeAirportIcao,
                    CodeAirportIata = CodeAirportIata,
                    Country = Country,
                    City = City,
                    TimeOffset = new DateTimeOffset(DateTime.UtcNow.Date, TimeOffsetInput.StartsWith("-") ? -offset : offset)
                };

                var createdItem = await _airportService.CreateAsync(dto);
                success = createdItem != null;
            }

            if (success)
            {
                DialogHost.Close("RootDialog");
                await _dialogService.ShowInfoAsync(IsEditMode ? "Aeropuerto actualizado exitosamente." : "Aeropuerto registrado exitosamente.");
                OnSavedResultAction?.Invoke();
            }
        }
        catch (Exception ex)
        {
             // Excepciones HTTP serán manejadas globalmente
            await _dialogService.ShowErrorAsync($"Error no controlado al guardar: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        DialogHost.Close("RootDialog");
    }

    private bool CanSave()
    {
        // Simple validations aligned with Heuristica 5
        return !string.IsNullOrWhiteSpace(NameOrganization) &&
               !string.IsNullOrWhiteSpace(Country) &&
               !string.IsNullOrWhiteSpace(City) &&
               !string.IsNullOrWhiteSpace(CodeAirportIata) &&
               !IsBusy;
    }
}
