using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var orders = await _orderService.GetAllAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            var result = CustomResponseDto<List<OrderDto>>.Success(HttpStatusCode.OK, orderDtos);
            return CreateActionResult(result);
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
            var response = await _orderService.OrderImportAsync(orderImports);
            return CreateActionResult(response);
        }
    }
}
