using LogoTransfer.Web.Models;
using LogoTransfer.Web.Models.UserModels;

namespace LogoTransfer.Web.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BaseAPI");
        }

        public async Task<UserModel> LogIn(SignInModel signIn)
        {
            var response = await _httpClient.PostAsJsonAsync("User/LogIn", signIn);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<ResponseModel<UserModel>>();
            return responseBody.Data;
        }
    }
}
