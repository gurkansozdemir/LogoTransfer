using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogoTransfer.API.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(HttpStatusCode.OK, userDtos));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn(SignInDto signInDto)
        {
            return CreateActionResult(await _userService.GetByUserNameAndPasswordAsync(signInDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(HttpStatusCode.OK, userDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            await _userService.RemoveAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser(InsertUserDto user)
        {
            User newUser = _mapper.Map<User>(user);
            await _userService.AddAsync(newUser);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(HttpStatusCode.OK));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetMenuItemsByRoleId(Guid id)
        {
            return CreateActionResult(await _userService.GetMenuItemsAsync(id));
        }
    }
}
