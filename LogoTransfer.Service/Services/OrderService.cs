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
        private readonly IProductService _productService;
        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IHttpClientFactory httpClient, IOrderRepository orderRepository, IMapper mapper, CacheData cacheData, ILogger<OrderService> logger, IProductService productService) : base(repository, unitOfWork)
        {
            _httpClient = httpClient.CreateClient("LOGOAPI");
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cacheData = cacheData;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        public async Task<CustomResponseDto<List<OrderDto>>> GetAllWithTransactions()
        {
            var orders = await _orderRepository.GetAllWithTransactions();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            if (_cacheData.ProductMatches == null)
            {
                await _productService.ProductMatchesSaveCacheAsync();
            }
            orderDtos.ForEach(o =>
            {
                o.Transactions.ForEach(t =>
                {
                    t.MasterCode = _cacheData.ProductMatches.Where(x => x.OtherCode == t.OtherCode).Select(x => x.Code).DefaultIfEmpty("").First();
                    t.IsProductMatch = _cacheData.ProductMatches.Where(x => x.OtherCode == t.OtherCode).Select(x => x.Code).DefaultIfEmpty("").First() != "" ? true : false;
                });
            });
            return CustomResponseDto<List<OrderDto>>.Success(HttpStatusCode.OK, orderDtos);
        }

        public async Task<string> GetLastPullTimeAsync()
        {
            var result = await _orderRepository.GetLastPullTimeAsync();
            string resultStr = result.ToString("yyyy-MM-dd HH:mm:ss");
            _logger.LogInformation("Data: {lastOrder} and convert string {string}", result, resultStr);
            return resultStr;
        }

        public async Task<CustomResponseDto<List<OrderTransactionDto>>> GetTransactionsByOrderId(Guid orderId)
        {
            var transactions = await _orderRepository.GetTransactionsByOrderId(orderId);
            var transactionDtos = _mapper.Map<List<OrderTransactionDto>>(transactions);
            if (_cacheData.ProductMatches == null)
            {
                await _productService.ProductMatchesSaveCacheAsync();
            }
            transactionDtos.ForEach(t =>
            {
                t.MasterCode = _cacheData.ProductMatches.Where(x => x.OtherCode == t.OtherCode).Select(x => x.Code).DefaultIfEmpty("").First();
                t.IsProductMatch = _cacheData.ProductMatches.Where(x => x.OtherCode == t.OtherCode).Select(x => x.Code).DefaultIfEmpty("").First() != "" ? true : false;
            });
            return CustomResponseDto<List<OrderTransactionDto>>.Success(HttpStatusCode.OK, transactionDtos);
        }

        public async Task<List<OrderImportResponseDto>> OrderImportAsync(List<OrderImportDto> orderImports)
        {
            var list = orderImports.Where(x => x.Transactions.Any(t => t.MasterCode != "")).ToList();
            var response = await _httpClient.PostAsJsonAsync("order", list);
            _logger.LogInformation("{time}: {action} end with httpclient response: {responseData}", DateTime.Now, nameof(OrderImportAsync), JsonSerializer.Serialize(response));

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<OrderImportResponseDto>>>();
            _logger.LogInformation("{time}: {action} end with response data: {responseData}", DateTime.Now, nameof(OrderImportAsync), JsonSerializer.Serialize(result));

            foreach (var item in result.Data)
            {
                if (String.IsNullOrEmpty(item.ReturnError))
                {
                    await SetIntegratedNo(item);
                }
            }

            return result.Data;
        }

        public async Task OrderLog(OrderLog log)
        {
            await _orderRepository.OrderLog(log);
        }

        public async Task SetIntegratedNo(OrderImportResponseDto importedOrder)
        {
            var order = await _orderRepository.GetOrderByNumber(importedOrder.Number);
            if (String.IsNullOrEmpty(importedOrder.ReturnError))
            {
                order.Integration = importedOrder.ReturnNumber;
                order.TransferStatus = true;
            }
            else
            {
                order.Integration = importedOrder.ReturnError;
                order.TransferStatus = false;
            }
            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddOrUpdateAsync(List<Order> orders)
        {
            foreach (var order in orders)
            {
                if (!await _orderRepository.CheckOrderNumberAsync(order.Number))
                {
                    await _orderRepository.AddAsync(order);
                }
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task AutoImportAsync()
        {
            var orders = await _orderRepository.GetNotImportedWithTransactions();
            var orderDtos = _mapper.Map<List<OrderImportDto>>(orders);
            if (_cacheData.ProductMatches == null)
            {
                await _productService.ProductMatchesSaveCacheAsync();
            }
            orderDtos.ForEach(o =>
            {
                o.Transactions.ForEach(t =>
                {
                    t.MasterCode = _cacheData.ProductMatches.Where(x => x.OtherCode == t.OtherCode).Select(x => x.Code).DefaultIfEmpty("").First();
                    t.IsProductMatch = _cacheData.ProductMatches.Where(x => x.OtherCode == t.OtherCode).Select(x => x.Code).DefaultIfEmpty("").First() != "" ? true : false;
                });
            });

            var orderableData = orderDtos.Where(x => x.Transactions.All(t => t.IsProductMatch)).ToList();
            await OrderImportAsync(orderableData);
        }
    }
}
