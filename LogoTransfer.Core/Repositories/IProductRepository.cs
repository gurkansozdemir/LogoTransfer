using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<OrderTransaction>
    {
        public Task<List<OrderTransaction>> GetByOrderIdAsync(Guid id);
        public Task<List<ProductMatching>> GetProductMatchAsync();
    }
}
