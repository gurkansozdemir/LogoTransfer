using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LogoTransfer.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DbSet<Order> _dbSet;
        private readonly DbSet<OrderTransaction> _dbSetTransaction;
        private readonly DbSet<OrderLog> _dbSetOrderLog;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _dbSet = context.Set<Order>();
            _dbSetTransaction = context.Set<OrderTransaction>();
            _dbSetOrderLog = context.Set<OrderLog>();
        }

        public async Task<List<Order>> GetAllWithTransactions()
        {
            return await _dbSet.Include(x => x.Transactions).ToListAsync();
        }

        public Task<DateTime> GetLastPullTimeAsync()
        {
            return _dbSetOrderLog.Where(x => x.Status).OrderByDescending(x => x.RunTime).Select(x => x.RunTime).FirstOrDefaultAsync();
        }

        public async Task<List<OrderTransaction>> GetTransactionsByOrderId(Guid orderId)
        {
            return await _dbSetTransaction.Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task OrderLog(OrderLog log)
        {
            await _dbSetOrderLog.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckOrderNumberAsync(string number)
        {
            return await _dbSet.AnyAsync(x => x.Number == number);
        }
    }
}
