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
        private readonly ILogger<UserController> _logger;

        public ProductController(IProductService productService, ILogger<UserController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            _logger.LogInformation("{time}: {action} başlatıldı.", DateTimeOffset.UtcNow, nameof(All));
            var products = await _productService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<List<Product>>.Success(HttpStatusCode.OK, products.ToList()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Product>.Success(HttpStatusCode.OK, product));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByOrderId(Guid id)
        {
            return CreateActionResult(await _productService.GetByOrderIdAsync(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetExternalProducts()
        {
            return CreateActionResult(await _productService.GetExternalProducts());
        }
    }
}
