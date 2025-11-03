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

        public ThirdPartyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SearchBooksAsync(string query)
        {
            var encodedQuery = System.Net.WebUtility.UrlEncode(query);

            var response = await _httpClient.GetStringAsync($"volumes?q={encodedQuery}");

            return response;
        }
    }
}