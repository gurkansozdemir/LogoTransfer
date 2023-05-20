using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Caching
{
    public class CacheData
    {
        public List<ProductMatching> ProductMatches { get; set; }
        private CustomResponseDto<List<ExternalProductDto>> externalProductDtos;
        public CustomResponseDto<List<ExternalProductDto>> ExternalProductDtos
        {
            get
            {
                if (externalProductDtos == null)
                {
                    GetExternalProducts();
                }
                return externalProductDtos;
            }
        }

        public LogoUserDto logoUser;
        public LogoUserDto LogoUser { get; set; }

        private readonly HttpClient _httpClient;
        public string Token { get; set; }

        public CacheData(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LOGOAPI");
        }

        public async Task StartAsync()
        {
            await GetExternalProducts();
        }

        public async Task GetExternalProducts()
        {
            externalProductDtos = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
        }
    }
}
