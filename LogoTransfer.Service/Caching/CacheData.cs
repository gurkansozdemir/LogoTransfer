using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text.Json;

namespace LogoTransfer.Service.Caching
{
    public class CacheData
    {
        public List<ProductMatching> ProductMatches { get; set; }
        private List<ExternalProductDto> externalProductDtos;
        public List<ExternalProductDto> ExternalProductDtos
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
            try
            {
                var result = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
                externalProductDtos = result.Data;
            }
            catch (Exception)
            {
                
            }           
        }
    }
}
