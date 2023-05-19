using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public OrderController(IOrderService orderService, ILogger logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            _logger.LogInformation("{time}:{action} start.", DateTime.Now, nameof(All));
            var orders = await _orderService.GetAllAsync();
            var result = CustomResponseDto<List<Order>>.Success(HttpStatusCode.OK, orders.ToList());
            _logger.LogInformation("{time}:{action} end. Response:{response}", DateTime.Now, nameof(All), result);
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
