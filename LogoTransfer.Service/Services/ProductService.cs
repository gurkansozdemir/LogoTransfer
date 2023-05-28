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
    public class ProductService : Service<OrderTransaction>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly CacheData _cacheData;
        public ProductService(IGenericRepository<OrderTransaction> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper, IHttpClientFactory httpClientFactory, CacheData cacheData) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("LOGOAPI");
            _cacheData = cacheData;
        }

        public async Task SyncMasterProductAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductMatchDto>>>("product");
            var productMatch = _mapper.Map<List<ProductMatching>>(result.Data);
            await _productRepository.SyncMasterProductAsync(productMatch);
        }

        public async Task MatchAsync(ProductMatchDto productMatchDto)
        {
            var productMatch = _mapper.Map<ProductMatching>(productMatchDto);
            await _productRepository.MatchAsync(productMatch);
            await ProductMatchesSaveCacheAsync();
        }

        public async Task ProductMatchesSaveCacheAsync()
        {
            var productMatches = await _productRepository.GetProductMatchesAsync();
            var productMatchDtos = _mapper.Map<List<ProductMatchDto>>(productMatches);
            _cacheData.ProductMatches = productMatchDtos;
        }

        public async Task<CustomResponseDto<List<ProductMatchDto>>> GetProductMatchesFromCacheAsync()
        {
            if (_cacheData.ProductMatches != null)
            {
                return CustomResponseDto<List<ProductMatchDto>>.Success(HttpStatusCode.OK, _cacheData.ProductMatches);
            }
            var productMatches = await _productRepository.GetProductMatchesAsync();
            var productMatchDtos = _mapper.Map<List<ProductMatchDto>>(productMatches);
            _cacheData.ProductMatches = productMatchDtos;
            return CustomResponseDto<List<ProductMatchDto>>.Success(HttpStatusCode.OK, productMatchDtos);
        }
    }
}
