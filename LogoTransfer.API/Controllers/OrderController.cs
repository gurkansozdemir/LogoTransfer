﻿using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IService<Order> _service;

        public OrderController(IService<Order> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var orders = await _service.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<Order>>.Success(HttpStatusCode.OK, orders.ToList()));
        }
    }
}
