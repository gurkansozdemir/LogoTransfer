using AutoMapper;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
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
            CreateMap<LogoUser, LogoUserDto>();
            CreateMap<SignInDto, User>();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderImportDto>().ReverseMap();
            CreateMap<ProductMatchDto, ProductMatching>().ReverseMap();
            CreateMap<OrderTransaction, OrderTransactionDto>().ReverseMap();
            CreateMap<OrderTransaction, OrderTransactionImportDto>().ReverseMap();
        }
    }
}
