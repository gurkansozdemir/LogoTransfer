using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.DTOs.UserDTOs;

namespace LogoTransfer.Core.Services
{
    public interface IAuthorizationService
    {
        public Task<CustomResponseDto<TokenDto>> GetIdeasoftToken(GetTokenModel model);
        public Task<CustomResponseDto<List<Order>>> GetOrders();
        public Task<CustomResponseDto<LogoUserDto>> GetLogoUserInfo();
    }
}