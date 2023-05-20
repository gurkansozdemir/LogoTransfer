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

        public ProductController(IProductService productService, ILogger<UserController> logger)
        {
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetExternalProducts()
        {
            return CreateActionResult(await _productService.GetExternalProducts());
        }
    }
}
