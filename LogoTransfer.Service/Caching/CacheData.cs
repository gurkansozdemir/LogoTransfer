using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Caching
{
    public class CacheData
    {
        public static List<ExternalProductDto> externalProductDtos;
        public static string Token { get; set; }

        private readonly HttpClient _httpClient;

        public CacheData(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LOGOAPI");
        }

        public async Task StartAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
            externalProductDtos = products.Data;
        }

    }
}
