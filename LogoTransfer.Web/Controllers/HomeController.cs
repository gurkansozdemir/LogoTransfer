using LogoTransfer.Web.Filters;
using LogoTransfer.Web.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LogoTransfer.Web.Controllers
{
    [Authorize(Roles = "Supervisor")]
    [SessionFilter]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var currentUser = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("CurrentUser"));
            return View(currentUser);
        }
    }
}