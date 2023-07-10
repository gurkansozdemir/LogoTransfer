using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace LogoTransfer.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IGenericRepository<LogoUser> _logoUserRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private CacheData _cacheData;
        private readonly IConfiguration _configuration;
        public AuthorizationService(IHttpClientFactory httpClientFactory, CacheData cacheData, IMapper mapper, IGenericRepository<LogoUser> logoUserRepository = null, IConfiguration configuration = null)
        {
            _httpClient = httpClientFactory.CreateClient("IdeaSoftAPI");
            _cacheData = cacheData;
            _mapper = mapper;
            _logoUserRepository = logoUserRepository;
            _configuration = configuration;
        }

        //public bool GetIdeasoftToken()
        //{ 
        //    string clientId = _configuration.GetSection("IdeaSoftClient:clientId").Value;
        //    string redirectUrl = _configuration.GetSection("IdeaSoftClient:redirectUrl").Value;
        //    string url = _httpClient.BaseAddress.ToString(); 
        //    string param = $"admin/user/auth?client_id={clientId}&response_type=code&state=teststatecode&redirect_uri={redirectUrl}";
        //    HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = _httpClient.PostAsync(url, content).Result;
             
        //    return true;
        //}

        public async Task<CustomResponseDto<TokenDto>> GetIdeasoftToken(GetTokenModel model)
        {
            string clientId = _configuration.GetSection("IdeaSoftClient:clientId").Value;
            string clientSecret = _configuration.GetSection("IdeaSoftClient:clientSecret").Value;
            string redirectUrl = _configuration.GetSection("IdeaSoftClient:redirectUrl").Value;
            TokenDto tokenDto = null;

            if (_cacheData.Created != null && _cacheData.Token != null && _cacheData.Created.Value.AddYears(1) < DateTime.Now)
            { _cacheData.Created = null; _cacheData.Token = null; }

            if (_cacheData.Created == null && _cacheData.Token == null)
            {
                tokenDto = await _httpClient.GetFromJsonAsync<TokenDto>(@$"oauth/v2/token?grant_type=authorization_code&client_id={clientId}&client_secret={clientSecret}&code={model.Code}&redirect_uri={redirectUrl}");
                _cacheData.Created = DateTime.Now;
            }

            if (_cacheData.Created.Value.AddHours(24) < DateTime.Now)
            {
                tokenDto = await _httpClient.GetFromJsonAsync<TokenDto>(@$"oauth/v2/token?grant_type=refresh_token&client_id={clientId}&client_secret={clientSecret}&refresh_token{_cacheData.RefreshToken}");
            }

            if (tokenDto == null)
            {
                tokenDto = new TokenDto 
                { 
                    Access_Token = _cacheData.Token,
                    Refresh_Token = _cacheData.RefreshToken
                }; 
            }

            _cacheData.Token = tokenDto.Access_Token;
            _cacheData.RefreshToken = tokenDto.Refresh_Token;
            return CustomResponseDto<TokenDto>.Success(HttpStatusCode.OK, tokenDto);
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cacheData.Token);
            var response = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>("api/orders");
            return CustomResponseDto<List<Core.DTOs.IdeaSoft.Order>>.Success(HttpStatusCode.OK, response);
        }

        public string GetIdeaSoftTokenFromCache()
        { 
            return _cacheData.Token;
        }
    }
}