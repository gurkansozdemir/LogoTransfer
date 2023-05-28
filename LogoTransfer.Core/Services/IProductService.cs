﻿using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IProductService : IService<OrderTransaction>
    {
        public CustomResponseDto<List<ExternalProductDto>> GetExternalProduct();
        public Task SyncMasterProductAsync();
        public Task MatchAsync(ProductMatchDto productMatch);
    }
}
