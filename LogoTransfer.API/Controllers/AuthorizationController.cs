using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.API.Controllers
{
    public class AuthorizationController : CustomBaseController
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetIdeasoftToken([FromQuery] GetTokenModel model)
        {
            return CreateActionResult(await _authorizationService.GetIdeasoftToken(model));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserInfo()
        {
            return CreateActionResult(await _authorizationService.GetLogoUserInfo());
        }
    }
}
