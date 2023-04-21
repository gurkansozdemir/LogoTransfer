using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.RoleDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using LogoTransfer.Service.Exceptions;
using System.Net;

namespace LogoTransfer.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserDto>> GetByUserNameAndPasswordAsync(SignInDto signInDto)
        {
            var signInUser = _mapper.Map<User>(signInDto);
            var result = await _userRepository.GetByUserNameAndPasswordAsync(signInUser);
            if (result != null)
            {
                if (result.Password == signInUser.Password)
                {
                    var resultDto = _mapper.Map<UserDto>(result);
                    return CustomResponseDto<UserDto>.Success(HttpStatusCode.OK, resultDto);
                }
                else
                {
                    return CustomResponseDto<UserDto>.Fail(HttpStatusCode.BadRequest, "Wrong Password");
                }
            }
            else
            {
                throw new ClientSideException($"{signInDto.EMail} is Not Register.");
            }
        }

        public async Task<CustomResponseDto<List<MenuItemDto>>> GetMenuItemsAsync(Guid roleId)
        {
            var menuItems = await _userRepository.GetMenuItemsAsync(roleId);
            var menuItemDtos = _mapper.Map<List<MenuItemDto>>(menuItems.MenuItems);
            return CustomResponseDto<List<MenuItemDto>>.Success(HttpStatusCode.OK, menuItemDtos.OrderBy(x => x.RowNumber).ToList());
        }
    }
}
