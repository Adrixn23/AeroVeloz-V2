using System.Net.Http;
using System.Net.Http.Json;
using AeroVeloz.Desktop.Models.DTOs.Organization;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Interfaces.Organization;

namespace AeroVeloz.Desktop.Services.Implementations.Organization
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDialogService _dialogService;

        public OrganizationService(IHttpClientFactory httpClientFactory, IDialogService dialogService)
        {
            _httpClientFactory = httpClientFactory;
            _dialogService = dialogService;
        }

        public async Task<IEnumerable<OrganizationDto>> GetOrganizationsByTypeAsync(string type)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                var response = await client.GetAsync($"api/organizations/type/{type}");

                if (response.IsSuccessStatusCode)
                {
                    var organizations = await response.Content.ReadFromJsonAsync<IEnumerable<OrganizationDto>>();
                    return organizations ?? Enumerable.Empty<OrganizationDto>();
                }

                await _dialogService.ShowErrorAsync("Error al obtener aerolíneas del servidor");
                return Enumerable.Empty<OrganizationDto>();
            }
            catch (Exception)
            {
                await _dialogService.ShowErrorAsync("No se pudo conectar con el servidor para obtener los datos de la organización.");
                return Enumerable.Empty<OrganizationDto>();
            }
        }

        public async Task<bool> BlockOrganizationAsync(int orgId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                var response = await client.PutAsync($"api/organizations/{orgId}/block", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                await _dialogService.ShowErrorAsync("Ocurrió un error inesperado al intentar cambiar el estado de la organización.");
                return false;
            }
        }
    }
}
