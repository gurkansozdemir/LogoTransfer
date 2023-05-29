using LogoTransfer.Web.Caching;
using LogoTransfer.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.Controllers
{
    [SessionFilter]
    public class OrderController : Controller
    {
        private readonly CacheData _cacheData;

        public OrderController(CacheData cacheData)
        {
            _cacheData = cacheData;
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
    }
}
