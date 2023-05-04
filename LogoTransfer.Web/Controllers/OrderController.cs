using LogoTransfer.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
