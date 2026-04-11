using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using AeroVeloz.Desktop.Models.DTOs.Airport;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using TimeZoneModel = AeroVeloz.Desktop.Models.AirportTimeZone;

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
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [MaxLength(50, ErrorMessage = "Máx 50 caracteres.")]
    private string _nameOrganization = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [NotifyDataErrorInfo]
    [EmailAddress(ErrorMessage = "Correo inválido.")]
    [MaxLength(200, ErrorMessage = "Máx 200 caracteres.")]
    private string _emailOrganization = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "Debe tener 4 cc.")]
    private string _codeAirportIcao = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Debe tener 3 cc.")]
    private string _codeAirportIata = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    private string _country = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Requerido.")]
    private string _city = string.Empty;

    public ObservableCollection<string> Countries { get; } = new ObservableCollection<string>
    {
        "República Dominicana",
        "Estados Unidos",
        "España",
        "Colombia",
        "México"
    };

    [ObservableProperty]
    private ObservableCollection<string> _cities = new();

    partial void OnCountryChanged(string? value)
    {
        Cities.Clear();
        City = string.Empty;

        switch (value)
        {
            case "República Dominicana":
                Cities.Add("Santo Domingo");
                Cities.Add("Punta Cana");
                Cities.Add("Santiago");
                SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "-04:00");
                break;
            case "Estados Unidos":
                Cities.Add("New York");
                Cities.Add("Miami");
                Cities.Add("Los Angeles");
                SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "-05:00"); // Standard EST example
                break;
            case "España":
                Cities.Add("Madrid");
                Cities.Add("Barcelona");
                SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "+01:00");
                break;
            case "Colombia":
                Cities.Add("Bogotá");
                Cities.Add("Medellín");
                SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "-05:00");
                break;
            case "México":
                Cities.Add("Ciudad de México");
                Cities.Add("Cancún");
                SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "-06:00");
                break;
        }
    }

    [ObservableProperty]
    private TimeZoneModel? _selectedTimeZone;

    [ObservableProperty]
    private IReadOnlyList<TimeZoneModel> _availableTimeZones = TimeZoneModel.GetValidTimeZones();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private bool _isActived;

    public AirportDetailViewModel(
        IAirportService airportService, 
        IDialogService dialogService)
    {
        _airportService = airportService;
        _dialogService = dialogService;
        SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "+00:00");
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
        IsActived = true;
        SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "+00:00");
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
        IsActived = airport.IsActived;

        var offset = airport.TimeOffset.Offset;
        SelectedTimeZone = AvailableTimeZones.FirstOrDefault(tz => tz.TimeSpan == offset) 
            ?? AvailableTimeZones.FirstOrDefault(tz => tz.Offset == "+00:00");
    }

    private string FormatTimeOffset(TimeSpan offset)
    {
        var sign = offset < TimeSpan.Zero ? "-" : "+";
        var absoluteOffset = offset < TimeSpan.Zero ? -offset : offset;
        return $"{sign}{absoluteOffset:hh\\:mm}";
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            var errors = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
            await _dialogService.ShowInfoAsync($"Por favor corrija los siguientes errores:\n{errors}", "Validación");
            return;
        }

        if (SelectedTimeZone == null)
        {
            await _dialogService.ShowWarningAsync("Por favor selecciona una zona horaria válida.");
            return;
        }

       
        var timeOffset = DateTimeOffset.UtcNow.ToOffset(SelectedTimeZone.TimeSpan);

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
                    TimeOffset = timeOffset,
                    TypeOrganization = "Airport",
                    IsActived = IsActived, 
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
                    TimeOffset = timeOffset
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
        return !string.IsNullOrWhiteSpace(NameOrganization) &&
               !string.IsNullOrWhiteSpace(Country) &&
               !string.IsNullOrWhiteSpace(City) &&
               !string.IsNullOrWhiteSpace(CodeAirportIata) &&
               !IsBusy;
    }
}
