using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace LogoTransfer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<OrderTransaction>, IProductRepository
    {
        private readonly DbSet<OrderTransaction> _dbset;
        private readonly DbSet<ProductMatching> _dbsetProductMatch;
        private readonly IUnitOfWork _unitOfWork;
        public ProductRepository(AppDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _dbset = context.Set<OrderTransaction>();
            _dbsetProductMatch = context.Set<ProductMatching>();
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderTransaction>> GetByOrderIdAsync(Guid id)
        {
            return await _dbset.Where(x => x.OrderId == id).ToListAsync();
        }

        public async Task<List<ProductMatching>> GetProductMatchAsync()
        {
            return await _dbsetProductMatch.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task MatchAsync(ProductMatching productMatch)
        {
            _dbsetProductMatch.Update(productMatch);
            await _unitOfWork.CommitAsync();
        }

        public async Task SyncMasterProductAsync(List<ProductMatching> productMatchings)
        {
            await _dbsetProductMatch.AddRangeAsync(productMatchings);
            await _unitOfWork.CommitAsync();
        }
    }
}
