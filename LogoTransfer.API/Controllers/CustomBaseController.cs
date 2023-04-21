using LogoTransfer.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LogoTransfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if ((int)response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)response.StatusCode
                };
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
