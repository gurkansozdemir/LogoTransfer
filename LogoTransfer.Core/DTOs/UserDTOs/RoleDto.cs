using LogoTransfer.Core.DTOs.RoleDTOs;

namespace LogoTransfer.Core.DTOs.UserDTOs
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MenuItemDto> MenuItems { get; set; }
    }
}
