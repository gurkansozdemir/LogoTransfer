using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IHttpClientFactory httpClient) : base(repository, unitOfWork)
        {
            _httpClient = httpClient.CreateClient("LOGOAPI");
        }

        public async Task<CustomResponseDto<List<OrderImportResponseDto>>> OrderImportAsync(List<OrderImportDto> orderImports)
        {
            var response = await _httpClient.PostAsJsonAsync("", orderImports);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<List<OrderImportResponseDto>>>();
        }
    }
}
