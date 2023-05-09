using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class ProductController : CustomBaseController
    {
        private readonly IService<Product> _service;

        public ProductController(IService<Product> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var orders = await _service.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<Product>>.Success(HttpStatusCode.OK, orders.ToList()));
        }
    }
}
