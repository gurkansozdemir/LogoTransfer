using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.RoleDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IUserService : IService<User>
    {
        Task<CustomResponseDto<UserDto>> GetByUserNameAndPasswordAsync(SignInDto user);
        Task<CustomResponseDto<List<MenuItemDto>>> GetMenuItemsAsync(Guid roleId);
    }
}
