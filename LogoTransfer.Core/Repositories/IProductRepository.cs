﻿using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<OrderTransaction>
    {
        public Task<List<OrderTransaction>> GetByOrderIdAsync(Guid id);
        public Task<List<ProductMatching>> GetProductMatchAsync();
        public Task SyncMasterProductAsync(List<ProductMatching> productMatchings);
        public Task MatchAsync(ProductMatching productMatch);
        public Task<List<ProductMatching>> GetProductMatchesAsync();
        public Task<ProductMatching> GetProductByCodeAsync(string masterCode);
    }
}
