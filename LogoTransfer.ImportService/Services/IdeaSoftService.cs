using AutoMapper;
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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public IdeaSoftService(IOrderService orderService, IProductService productService, IMapper mapper, IHttpClientFactory httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient.CreateClient("IdeaSoftAPI");
            _orderService = orderService;
            _productService = productService;
        }
        public async Task SaveOrdersAsync()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>("api/orders");
            var baseOrders = _mapper.Map<List<Order>>(orders);
            await _orderService.AddRangeAsync(baseOrders);
        }

        public async Task SaveProductsAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>("api/products");
            var baseProducts = _mapper.Map<List<Product>>(products);
            await _productService.AddRangeAsync(baseProducts);
        }

        public void StartAsync(Object state)
        {
            SaveOrdersAsync();
            SaveProductsAsync();
        }
    }
}