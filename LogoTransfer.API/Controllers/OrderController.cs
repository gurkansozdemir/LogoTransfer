using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IAuthorizationService _authorizationService;

        public OrderController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _authorizationService.GetOrders());
        }
    }
}
