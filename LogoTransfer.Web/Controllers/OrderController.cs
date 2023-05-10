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
    }
}
