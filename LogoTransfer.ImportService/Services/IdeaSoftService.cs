using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogoTransfer.ImportService.Services
{
    public class IdeaSoftService : IImportService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;
        private readonly CacheDataImportService _cacheData;

        public IdeaSoftService(IOrderService orderService, IHttpClientFactory httpClient, CacheDataImportService cacheData)
        {

            _httpClient = httpClient.CreateClient("IdeaSoftAPI");
            _orderService = orderService;
            _cacheData = cacheData;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cacheData.Token);
        }
        public async Task SaveOrdersAsync()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>("api/orders");
            List<Order> baseOrders = new List<Order>();
            foreach (Core.DTOs.IdeaSoft.Order item in orders)
            {
                baseOrders.Add(new Order()
                {
                    Amount = (decimal)item.Amount,
                    CreatedOn = item.CreatedAt,
                    Currency = item.Currency,
                    CustomerFirstName = item.CustomerFirstname,
                    CustomerLastName = item.CustomerSurname,
                    OrderNo = item.TransactionId,
                    StoreName = "IdeaSoft",
                    StoreId = new Guid("b38e60bb-2bbe-4c7a-b47b-68eaabb7eeff"),
                    IsDeleted = false,
                    Integration = "",
                    TransferStatus = ""
                });
            }
            await _orderService.AddRangeAsync(baseOrders);
        }
    }
}