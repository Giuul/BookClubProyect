using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.Clients
{
    public class ThirdPartyApiClient : IThirdPartyApiClient
    {
        private readonly HttpClient _httpClient;

        // HttpClientFactory inyecta el cliente configurado
        public ThirdPartyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetSomeDataAsync(string resourceId)
        {
            // Aquí haces la llamada a la API de terceros
            var response = await _httpClient.GetStringAsync($"/api/v1/data/{resourceId}");
            return response;
        }
    }
}