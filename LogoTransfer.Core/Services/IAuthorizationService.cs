using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;

namespace LogoTransfer.Core.Services
{
    public interface IAuthorizationService
    {
        public Task<CustomResponseDto<TokenDto>> GetIdeasoftToken(GetTokenModel model);
        public Task<CustomResponseDto<List<Order>>> GetOrders();
    }
}
