using AeroVeloz.Domain.DomainServices.Interfaces.Airport;

namespace AeroVeloz.Infraestructure.Integrations.AirlineLabs
{

    public class AiportExternarDomainServiceValidator : IAiportExternarDomainServiceValidator
    {
        private readonly HttpClient? _http;
        private readonly AirLabsSettings? _settings;

        //configurar servicio de solicitud del contenedor de la api y elemento client de la api
        public AiportExternarDomainServiceValidator(HttpClient? http, AirLabsSettings? settings)
        {
            _http = http;
            _settings = settings;
        }


        //validar que el aeropuerto que se esta intentando registrar realmente existe en la vida real
        public async Task<bool> ValidateAirport(string iata, string icao)
        {
            var url = $"{_settings?.BaseUrl}airports?iata_code={iata}&api_key={_settings!.ApiKey}";

            var response = await _http!.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();

            return json.Contains(iata);
        }
    }
}
