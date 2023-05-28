using LogoTransfer.Web.Caching;
using LogoTransfer.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.Controllers
{
    [SessionFilter]
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMasterProducts()
        {
            var response = new { Data = CacheData.GetMasterProducts().Result };
            return Json(response);
        }
    }
}
