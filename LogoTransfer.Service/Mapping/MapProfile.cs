﻿using AutoMapper;
using LogoTransfer.Core.DTOs.RoleDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<InsertUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<SignInDto, User>();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemDto>();
        }
    }
}