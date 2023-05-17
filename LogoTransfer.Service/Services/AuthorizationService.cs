using AutoMapper;
using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Repositories;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IGenericRepository<LogoUser> _logoUserRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private CacheData _cacheData;
        public AuthorizationService(IHttpClientFactory httpClientFactory, CacheData cacheData, IMapper mapper, IGenericRepository<LogoUser> logoUserRepository = null)
        {
            _httpClient = httpClientFactory.CreateClient("IdeaSoftAPI");
            _cacheData = cacheData;
            _mapper = mapper;
            _logoUserRepository = logoUserRepository;
        }
        public async Task<CustomResponseDto<TokenDto>> GetIdeasoftToken(GetTokenModel model)
        {
            string clientId = "ntmyj19qnbk8g8skwwsgkw0kg8ww084ckswkgwk0w80owgwso";
            string clientSecret = "4qub43qah2qsw48k8ks0k0c00g4wcg080kcc4ss44kwkss0okg";
            string redirectUrl = "https://localhost:7096/api/authorization/getIdeasoftToken";

            var response = await _httpClient.GetFromJsonAsync<TokenDto>(@$"oauth/v2/token?grant_type=authorization_code&client_id={clientId}&client_secret={clientSecret}&code={model.Code}&redirect_uri={redirectUrl}");
            _cacheData.Token = response.Access_Token;
            return CustomResponseDto<TokenDto>.Success(HttpStatusCode.OK, response);
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
    }
}