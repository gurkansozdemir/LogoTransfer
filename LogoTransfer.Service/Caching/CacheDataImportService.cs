using System.Text;

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
            //string clientId = "ntmyj19qnbk8g8skwwsgkw0kg8ww084ckswkgwk0w80owgwso";
            //string redirectUrl = "4qub43qah2qsw48k8ks0k0c00g4wcg080kcc4ss44kwkss0okg";
            //string url = "https://formaram.myideasoft.com";
            //string param = $"/admin/user/auth?client_id={clientId}&response_type=code&state=teststatecode&redirect_uri={redirectUrl}";

            //HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");
            //try
            //{
            //    HttpClient client = new HttpClient();
            //    HttpResponseMessage response = await client.PostAsync(url + param, content);
            //    response.EnsureSuccessStatusCode();
            //    string responseBody = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(responseBody);
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine("Message :{0} ", e.Message);
            //}
            Token = await _httpClient.GetStringAsync("authorization/getIdeaSoftTokenFromCache");
        }
    }
}