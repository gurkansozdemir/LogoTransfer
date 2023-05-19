namespace LogoTransfer.Service.Caching
{
    public class CacheDataImportService
    {
        public string Token { get; set; }
        private readonly HttpClient _httpClient;
        public CacheDataImportService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LogoTransferAPI");
        }

        public async Task StartAsync()
        {
            Token = await _httpClient.GetStringAsync("authorization/getIdeaSoftTokenFromCache");
        }
    }
}