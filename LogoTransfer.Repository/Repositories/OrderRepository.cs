using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;

namespace LogoTransfer.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {

        }
    }
}
