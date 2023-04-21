using LogoTransfer.Web.Models;
using LogoTransfer.Web.Models.RoleModels;
using LogoTransfer.Web.Models.UserModels;

namespace LogoTransfer.Web.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> LogIn(SignInModel signIn)
        {
            var response = await _httpClient.PostAsJsonAsync("User/LogIn", signIn);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<ResponseModel<UserModel>>();
            return responseBody.Data;
        }

        public async Task<List<MenuItemModel>> GetMenus(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<MenuItemModel>>>($"User/GetMenuItemsByRoleId/{id}");
            return response.Data;
        }
    }
}
