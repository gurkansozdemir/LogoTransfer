using LogoTransfer.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.Controllers
{
    [Authorize(Roles = "Supervisor")]
    [SessionFilter]
    public class UserController : Controller
    {
        public IActionResult AllUser()
        {
            return View();
        }
    }
}
