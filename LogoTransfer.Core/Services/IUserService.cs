using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.RoleDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IUserService : IService<User>
    {
        public Task<CustomResponseDto<UserDto>> GetByUserNameAndPasswordAsync(SignInDto user);
        public Task<CustomResponseDto<List<MenuItemDto>>> GetMenuItemsAsync(Guid roleId);
        public Task<CustomResponseDto<List<UserDto>>> AllWithRoleAsync();
    }
}
