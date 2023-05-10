using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var orders = await _productService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<Product>>.Success(HttpStatusCode.OK, orders.ToList()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _productService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Product>.Success(HttpStatusCode.OK, order));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByOrderId(Guid id)
        {
            return CreateActionResult(await _productService.GetByOrderIdAsync(id));
        }
    }
}
