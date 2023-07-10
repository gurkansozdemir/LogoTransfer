using LogoTransfer.Web.Caching;
using LogoTransfer.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.Controllers
{
    [Authorize]
    [SessionFilter]
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly CacheData _cacheData;

        public OrderController(CacheData cacheData, IHttpClientFactory httpClient)
        {
            _cacheData = cacheData;
            _httpClient = httpClient.CreateClient("BaseAPI");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMasterProducts()
        {
            var response = new { Data = CacheData.GetMasterProducts() };
            return Json(response);
        }

        [HttpGet]
        public async void UpdateMasterProductsInCache()
        {
            await _cacheData.MasterProductSaveCache();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMasterProductsInCacheAll()
        {
            await _httpClient.GetAsync("Product/ClearProductMatchesFromCache");
            await _cacheData.MasterProductSaveCache();
            return RedirectToAction("Index");
        }
    }
}
