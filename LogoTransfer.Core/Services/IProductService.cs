﻿using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        public Task<CustomResponseDto<List<ProductDto>>> GetByOrderIdAsync(Guid id);
        public Task<CustomResponseDto<List<ExternalProductDto>>> GetExternalProducts();
        
    }
}
