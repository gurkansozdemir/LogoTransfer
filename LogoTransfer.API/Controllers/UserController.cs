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
        private readonly ILogger<UserController> _logger;

        public UserController(IMapper mapper, IUserService userService, ILogger<UserController> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(HttpStatusCode.OK, userDtos));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> AllWithRole()
        {
            return CreateActionResult(await _userService.AllWithRoleAsync());
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

        [HttpPost]
        public async Task<IActionResult> AddUser(InsertUserDto user)
        {
            User newUser = _mapper.Map<User>(user);
            await _userService.AddAsync(newUser);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(HttpStatusCode.OK));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(InsertUserDto userDto)
        {
            //var updatedUser = await _userService.GetByIdAsync(user.Id);
            //updatedUser.FirstName= user.FirstName;
            //updatedUser.LastName= user.LastName;
            //updatedUser.EMail = user.EMail;
            //updatedUser.Password = user.Password;
            //updatedUser.UpdatedOn = DateTime.Now;
            //updatedUser.RoleId = user.RoleId;
            var user = _mapper.Map<User>(userDto);
            await _userService.UpdateAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetMenuItemsByRoleId(Guid id)
        {
            return CreateActionResult(await _userService.GetMenuItemsAsync(id));
        }
    }
}
