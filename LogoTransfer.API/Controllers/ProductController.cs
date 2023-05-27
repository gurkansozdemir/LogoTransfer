using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using LogoTransfer.Core.DTOs.ProductDTOs;

namespace LogoTransfer.API.Controllers
{
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly CacheData _cacheData;

        public ProductController(IProductService productService, ILogger<ProductController> logger, CacheData cacheData)
        {
            _productService = productService;
            _logger = logger;
            _cacheData = cacheData;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetExternalProducts()
        {
            _logger.LogInformation("{time}: {action} run", DateTime.Now, nameof(GetExternalProducts));

            _logger.LogInformation("{time}: {action} end with response first example data: {responseData}", 
                DateTime.Now, nameof(GetExternalProducts),
            JsonSerializer.Serialize(_cacheData.ExternalProductDtos.FirstOrDefault()));

            return CreateActionResult(CustomResponseDto<List<ExternalProductDto>>.Success(HttpStatusCode.OK, _cacheData.ExternalProductDtos)); 
        }
    }
}
