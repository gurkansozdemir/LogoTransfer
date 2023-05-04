using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.DTOs.IdeaSoft;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogoTransfer.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        public AuthorizationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IdeaSoftAPI");
        }
        public async Task<CustomResponseDto<TokenDto>> GetIdeasoftToken(GetTokenModel model)
        {
            string clientId = "ntmyj19qnbk8g8skwwsgkw0kg8ww084ckswkgwk0w80owgwso";
            string clientSecret = "4qub43qah2qsw48k8ks0k0c00g4wcg080kcc4ss44kwkss0okg";
            string redirectUrl = "https://localhost:7096/api/authorization/getIdeasoftToken";

            var response = await _httpClient.GetFromJsonAsync<TokenDto>(@$"oauth/v2/token?grant_type=authorization_code&client_id={clientId}&client_secret={clientSecret}&code={model.Code}&redirect_uri={redirectUrl}");
            CacheData.Token = response.Access_Token;
            return CustomResponseDto<TokenDto>.Success(HttpStatusCode.OK, response);
        }

        public async Task<CustomResponseDto<List<Order>>> GetOrders()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CacheData.Token);
            var response = await _httpClient.GetFromJsonAsync<List<Order>>("api/orders");
            return CustomResponseDto<List<Order>>.Success(HttpStatusCode.OK, response);
        }
    }
}
