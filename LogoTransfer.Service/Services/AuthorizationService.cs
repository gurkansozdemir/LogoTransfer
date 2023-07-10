using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Core.UnitOfWorks;
using LogoTransfer.Service.Caching;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IGenericRepository<LogoUser> _logoUserRepository;
        private readonly IGenericRepository<Token> _tokenRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private CacheData _cacheData;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorizationService(IHttpClientFactory httpClientFactory, CacheData cacheData, IMapper mapper, IGenericRepository<LogoUser> logoUserRepository = null, IConfiguration configuration = null, IGenericRepository<Token> tokenRepository = null, IUnitOfWork unitOfWork = null)
        {
            _httpClient = httpClientFactory.CreateClient("IdeaSoftAPI");
            _cacheData = cacheData;
            _mapper = mapper;
            _logoUserRepository = logoUserRepository;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<NoContentDto>> GetIdeasoftToken(GetTokenModel model)
        {
            string clientId = _configuration.GetSection("IdeaSoftClient:clientId").Value;
            string clientSecret = _configuration.GetSection("IdeaSoftClient:clientSecret").Value;
            string redirectUrl = _configuration.GetSection("IdeaSoftClient:redirectUrl").Value;

            var token = await _httpClient.GetFromJsonAsync<Token>(@$"oauth/v2/token?grant_type=authorization_code&client_id={clientId}&client_secret={clientSecret}&code={model.Code}&redirect_uri={redirectUrl}");

            _cacheData.Token = token;

            await _tokenRepository.AddAsync(token);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(HttpStatusCode.OK);
        }

        public void RefreshIdeasoftToken()
        {
            string clientId = _configuration.GetSection("IdeaSoftClient:clientId").Value;
            string clientSecret = _configuration.GetSection("IdeaSoftClient:clientSecret").Value;
            var refreshToken = _tokenRepository.GetAll().OrderByDescending(x => x.CreatedOn).FirstOrDefault();
            if (refreshToken.CreatedOn < refreshToken.CreatedOn.AddDays(50))
            {
                var token = _httpClient.GetFromJsonAsync<Token>(@$"oauth/v2/token?grant_type=refresh_token&client_id={clientId}&client_secret={clientSecret}&refresh_token={refreshToken.Refresh_Token}");
                _cacheData.Token = token.Result;

                _tokenRepository.AddAsync(token.Result);
                _unitOfWork.CommitAsync();
            }
        }

        public async Task<CustomResponseDto<LogoUserDto>> GetLogoUserInfo()
        {
            if (_cacheData.LogoUser != null)
            {
                return CustomResponseDto<LogoUserDto>.Success(HttpStatusCode.OK, _cacheData.LogoUser);
            }
            else
            {
                var logoUser = _logoUserRepository.GetAll().SingleOrDefault();
                var logoUserDto = _mapper.Map<LogoUserDto>(logoUser);
                _cacheData.LogoUser = logoUserDto;
                return CustomResponseDto<LogoUserDto>.Success(HttpStatusCode.OK, logoUserDto);
            }
        }

        public async Task<CustomResponseDto<List<Core.DTOs.IdeaSoft.Order>>> GetOrders()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cacheData.Token.Access_Token);
            var response = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>("api/orders");
            return CustomResponseDto<List<Core.DTOs.IdeaSoft.Order>>.Success(HttpStatusCode.OK, response);
        }

        public async Task<CustomResponseDto<Token>> GetIdeaSoftTokenFromCache()
        {
            if (_cacheData.Token.CreatedOn > _cacheData.Token.CreatedOn.AddHours(20))
            {
                RefreshIdeasoftToken();
                return await GetIdeaSoftTokenFromCache();
            }
            return CustomResponseDto<Token>.Success(HttpStatusCode.OK, _cacheData.Token);
        }
    }
}