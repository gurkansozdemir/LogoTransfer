using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using LogoTransfer.Service.Caching;
using System.Net;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly CacheData _cacheData;
        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IHttpClientFactory httpClient, IOrderRepository orderRepository, IMapper mapper, CacheData cacheData) : base(repository, unitOfWork)
        {
            _httpClient = httpClient.CreateClient("LOGOAPI");
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cacheData = cacheData;
        }

        public async Task<CustomResponseDto<List<OrderTransactionDto>>> GetTransactionsByOrderId(Guid orderId)
        {
            var transactions = await _orderRepository.GetTransactionsByOrderId(orderId);
            var transactionDtos = _mapper.Map<List<OrderTransactionDto>>(transactions);
            //transactionDtos.ForEach(x => x.IsProductMatch = _cacheData.ProductMatches.Exists(pm => pm.OtherProductCode == x.OtherCode && !pm.IsDeleted));
            return CustomResponseDto<List<OrderTransactionDto>>.Success(HttpStatusCode.OK, transactionDtos);
        }

        public async Task<CustomResponseDto<List<OrderImportResponseDto>>> OrderImportAsync(List<OrderImportDto> orderImports)
        {
            var response = await _httpClient.PostAsJsonAsync("", orderImports);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<List<OrderImportResponseDto>>>();
        }
    }
}
