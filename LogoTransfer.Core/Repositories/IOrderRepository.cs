using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<List<OrderTransaction>> GetTransactionsByOrderId(Guid orderId);
        public Task<List<Order>> GetAllWithTransactions();
        public Task OrderLog(OrderLog log);
        public Task<DateTime> GetLastPullTimeAsync();
    }
}
