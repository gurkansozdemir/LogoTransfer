using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<List<OrderTransaction>> GetTransactionsByOrderId(Guid orderId);
        public Task<List<Order>> GetAllWithTransactions();
        public Task OrderLog(OrderLog log);
        public Task<DateTime> GetLastPullTimeAsync();
        public Task<bool> CheckOrderNumberAsync(string number);
        public Task<List<Order>> GetNotImportedWithTransactions();
        public Task<Order> GetOrderByNumber(string number);
    }
}
