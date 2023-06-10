using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.DTOs.OrderDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IOrderService : IService<Order>
    {
        public Task<CustomResponseDto<List<OrderTransactionDto>>> GetTransactionsByOrderId(Guid orderId);
        public Task<List<OrderImportResponseDto>> OrderImportAsync(List<OrderImportDto> orderImports);
        public Task<CustomResponseDto<List<OrderDto>>> GetAllWithTransactions();
        public Task OrderLog(OrderLog log);
        public Task<string> GetLastPullTimeAsync();
    }
}
