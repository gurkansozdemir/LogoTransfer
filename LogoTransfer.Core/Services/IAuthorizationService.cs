using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IAuthorizationService
    {
        public Task<CustomResponseDto<NoContentDto>> GetIdeasoftToken(GetTokenModel model);
        public Task<CustomResponseDto<List<DTOs.IdeaSoft.Order>>> GetOrders();
        public Task<CustomResponseDto<LogoUserDto>> GetLogoUserInfo();
        public Task<CustomResponseDto<Token>> GetIdeaSoftTokenFromCache();
    }
}