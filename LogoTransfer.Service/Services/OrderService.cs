using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using LogoTransfer.Service.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace LogoTransfer.Service.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly CacheData _cacheData;
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IHttpClientFactory httpClient, IOrderRepository orderRepository, IMapper mapper, CacheData cacheData, ILogger<OrderService> logger) : base(repository, unitOfWork)
        {
            _httpClient = httpClient.CreateClient("LOGOAPI");
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cacheData = cacheData;
            _logger = logger;
            _unitOfWork = unitOfWork;
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
            var response = await _httpClient.PostAsJsonAsync("order", orderImports);
            _logger.LogInformation("{time}: {action} end with httpclient response: {responseData}", DateTime.Now, nameof(OrderImportAsync), JsonSerializer.Serialize(response));

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<OrderImportResponseDto>>>();
            _logger.LogInformation("{time}: {action} end with response data: {responseData}", DateTime.Now, nameof(OrderImportAsync), JsonSerializer.Serialize(result));

            await SetIntegratedNo(result.Data);
            return result;
        }

        public async Task SetIntegratedNo(List<OrderImportResponseDto> importedOrder)
        {
            var orders = _orderRepository.GetAll();
            foreach (var item in importedOrder)
            {
                var order = await orders.Where(x => x.Number == item.Number).SingleAsync();
                order.Integration = item.ReturnNumber;
                order.TransferStatus = true;
                _orderRepository.Update(order);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
