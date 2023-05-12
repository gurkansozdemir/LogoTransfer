using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LogoTransfer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _dbset;
        private readonly DbSet<ProductMatching> _dbsetProductMatch;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _dbset = context.Set<Product>();
            _dbsetProductMatch = context.Set<ProductMatching>();
        }

        public async Task<List<Product>> GetByOrderIdAsync(Guid id)
        {
            return await _dbset.Where(x => x.OrderId == id && !x.IsDeleted).ToListAsync();
        }

        public async Task<List<ProductMatching>> GetProductMatchAsync()
        {
            return await _dbsetProductMatch.Where(x => !x.IsDeleted).ToListAsync();
        }
    }
}
