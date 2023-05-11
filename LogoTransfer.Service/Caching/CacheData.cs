using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Caching
{
    public class CacheData
    {
        public static CustomResponseDto<List<ExternalProductDto>> ExternalProductDtos { get; set; }
        public static string Token { get; set; }

        private readonly HttpClient _httpClient;

        public CacheData(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LOGOAPI");
        }

        public async Task StartAsync()
        {
            ExternalProductDtos = await GetExternalProducts();
        }

        public async Task<CustomResponseDto<List<ExternalProductDto>>> GetExternalProducts()
        {
            return await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
        }

    }
}
