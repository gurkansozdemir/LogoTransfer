using LogoTransfer.Web.Caching;
using LogoTransfer.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string roleName)
        {
            return View("_MenuPartial", await CacheData.GetMenuItemsByRole(roleName));
        }
    }
}
