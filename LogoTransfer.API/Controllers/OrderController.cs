using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace LogoTransfer.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            var result = CustomResponseDto<List<OrderDto>>.Success(HttpStatusCode.OK, orderDtos);
            return CreateActionResult(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllWithTransactions()
        {
            var orders = await _orderService.GetAllWithTransactions();
            return CreateActionResult(orders);
        }

        [HttpGet("[action]/{orderId}")]
        public async Task<IActionResult> GetTransactionsByOrderId(Guid orderId)
        {
            var result = await _orderService.GetTransactionsByOrderId(orderId);
            return CreateActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> OrderImport(List<OrderImportDto> orderImports)
        {
            _logger.LogInformation("{time}: {action} run with request data: {requestData}", DateTime.Now, nameof(OrderImport), JsonSerializer.Serialize(orderImports));
            var response = await _orderService.OrderImportAsync(orderImports);
            _logger.LogInformation("{time}: {action} end with response data: {responseData}", DateTime.Now, nameof(OrderImport), JsonSerializer.Serialize(response));
            return CreateActionResult(CustomResponseDto<List<OrderImportResponseDto>>.Success(HttpStatusCode.OK, response));
        }
    }
}
