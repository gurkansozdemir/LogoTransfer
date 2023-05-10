using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<List<Product>> GetByOrderIdAsync(Guid id);
    }
}
