using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IProductService : IService<OrderTransaction>
    {
        public Task SyncMasterProductAsync();
        public Task MatchAsync(ProductMatchDto productMatch);
        public Task<CustomResponseDto<List<ProductMatchDto>>> GetProductMatchesFromCacheAsync();
        public Task ProductMatchesSaveCacheAsync();
    }
}
