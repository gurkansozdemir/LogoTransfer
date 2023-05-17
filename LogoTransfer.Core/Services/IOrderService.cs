using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IOrderService : IService<Order>
    {
        public Task<CustomResponseDto<List<OrderImportResponseDto>>> OrderImportAsync(List<OrderImportDto> orderImports);
    }
}
