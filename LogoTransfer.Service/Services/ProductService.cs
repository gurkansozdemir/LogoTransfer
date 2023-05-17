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
        private readonly CacheData _cacheData;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper, IHttpClientFactory httpClientFactory, CacheData cacheData) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("LOGOAPI");
            _cacheData = cacheData;
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetByOrderIdAsync(Guid id)
        {
            var products = await _productRepository.GetByOrderIdAsync(id);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            var productMatch = await _productRepository.GetProductMatchAsync();
            productDtos.ForEach(x => x.IsMatch = productMatch.Exists(pm => pm.ProductCode == x.Code && !pm.IsDeleted));

            return CustomResponseDto<List<ProductDto>>.Success(HttpStatusCode.OK, productDtos);
        }

        public async Task<CustomResponseDto<List<ExternalProductDto>>> GetExternalProducts()
        {
            if (_cacheData.ExternalProductDtos.Data.Count() > 0)
            {
                return _cacheData.ExternalProductDtos;
            }
            return await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
        }
    }
}
