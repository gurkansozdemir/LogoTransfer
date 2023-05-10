using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using System.Net;

namespace LogoTransfer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetByOrderIdAsync(Guid id)
        {
            var products = await _productRepository.GetByOrderIdAsync(id);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return CustomResponseDto<List<ProductDto>>.Success(HttpStatusCode.OK, productDtos);
        }
    }
}
