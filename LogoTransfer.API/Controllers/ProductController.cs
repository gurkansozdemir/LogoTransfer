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

            var response = _productService.GetExternalProduct();

            _logger.LogInformation("{time}: {action} end with response data count: {responseDataCount}",
                DateTime.Now, nameof(GetExternalProducts),
                response.Data.Count);

            return CreateActionResult(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SyncMasterProduct()
        {
            _logger.LogInformation("{time}: {action} run", DateTime.Now, nameof(SyncMasterProduct));

            await _productService.SyncMasterProductAsync();

            _logger.LogInformation("{time}: {action} end", DateTime.Now, nameof(SyncMasterProduct));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }
    }
}
