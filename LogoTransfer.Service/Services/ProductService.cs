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

        public CustomResponseDto<List<ExternalProductDto>> GetExternalProduct()
        {
            var data = _cacheData.ExternalProductDtos;
            return CustomResponseDto<List<ExternalProductDto>>.Success(HttpStatusCode.OK, data);
        }

        public async Task SyncMasterProductAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ExternalProductDto>>>("product");
            var productMatch = _mapper.Map<List<ProductMatching>>(result.Data);
            await _productRepository.SyncMasterProductAsync(productMatch);
        }
    }
}
