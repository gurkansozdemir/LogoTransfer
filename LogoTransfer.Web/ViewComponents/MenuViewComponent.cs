using LogoTransfer.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly UserApiService _userApiService;
        public MenuViewComponent(UserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid roleId)
        {
            return View("_MenuPartial", await _userApiService.GetMenus(roleId));
        }
    }
}
