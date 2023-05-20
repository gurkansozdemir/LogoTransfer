using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LogoTransfer.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DbSet<Order> _dbSet;
        private readonly DbSet<OrderTransaction> _dbSetTransaction;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _dbSetTransaction = context.Set<OrderTransaction>();
        }

        public async Task<List<OrderTransaction>> GetTransactionsByOrderId(Guid orderId)
        {
            return await _dbSetTransaction.Where(x => x.OrderId == orderId).ToListAsync();
        }
    }
}
