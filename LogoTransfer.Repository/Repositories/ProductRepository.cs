using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LogoTransfer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _dbset;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _dbset = context.Set<Product>();
        }

        public async Task<List<Product>> GetByOrderIdAsync(Guid id)
        {
            return await _dbset.Where(x => x.OrderId == id && !x.IsDeleted).ToListAsync();
        }
    }
}
