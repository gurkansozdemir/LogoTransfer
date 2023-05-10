using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var orders = await _orderService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<Order>>.Success(HttpStatusCode.OK, orders.ToList()));
        }
    }
}
