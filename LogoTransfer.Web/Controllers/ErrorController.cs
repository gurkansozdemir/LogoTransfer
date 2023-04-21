using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
