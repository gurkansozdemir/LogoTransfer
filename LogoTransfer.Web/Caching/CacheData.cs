﻿using LogoTransfer.Web.Models;
using LogoTransfer.Web.Models.RoleModels;

namespace LogoTransfer.Web.Caching
{
    public class CacheData
    {
        public static List<MenuItemModel> SupervisorMenuItems;
        public static List<MenuItemModel> StandartUserMenuItems;
        private readonly HttpClient _httpClient;

        public CacheData(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LogoTransferAPI");
        }

        public async Task MenuItemsSaveCash()
        {
            if (SupervisorMenuItems == null)
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<MenuItemModel>>>($"User/GetMenuItemsByRoleId/45456c11-f1f1-447b-a55d-c8f4110da3fe");
                SupervisorMenuItems = response.Data;
            }
            if (StandartUserMenuItems == null)
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<MenuItemModel>>>($"User/GetMenuItemsByRoleId/7e212bbe-3059-464f-be67-ec8064063f6b");
                StandartUserMenuItems = response.Data;
            }
        }

        public static async Task<List<MenuItemModel>> GetMenuItemsByRole(string roleName)
        {
            switch (roleName)
            {
                case "Supervisor":
                    return SupervisorMenuItems;
                case "StandartUser":
                    return StandartUserMenuItems;
                default:
                    return new List<MenuItemModel>();
            }
        }
    }
}
