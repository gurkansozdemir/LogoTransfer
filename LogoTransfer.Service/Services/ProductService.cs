using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using LogoTransfer.Service.Caching;
using System.Net;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper, IHttpClientFactory httpClientFactory) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("LOGOAPI");
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetByOrderIdAsync(Guid id)
        {
            var products = await _productRepository.GetByOrderIdAsync(id);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return CustomResponseDto<List<ProductDto>>.Success(HttpStatusCode.OK, productDtos);
        }

        public async Task<CustomResponseDto<List<ExternalProductDto>>> GetExternalProducts()
        {
            if (CacheData.ExternalProductDtos.Data.Count() > 0)
            {
                return CacheData.ExternalProductDtos;
            }
            return await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
        }
    }
}
