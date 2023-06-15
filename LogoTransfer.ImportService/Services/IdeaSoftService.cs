using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LogoTransfer.ImportService.Services
{
    public class IdeaSoftService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;
        private readonly CacheDataImportService _cacheData;
        private static System.Timers.Timer _timer;
        private readonly Dictionary<string, string> _orderStatus;

        public IdeaSoftService(IOrderService orderService, IHttpClientFactory httpClient, CacheDataImportService cacheData)
        {

            _httpClient = httpClient.CreateClient("IdeaSoftAPI");
            _orderService = orderService;
            _cacheData = cacheData;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cacheData.Token);
            //_timer = new System.Timers.Timer(1000);
            //_timer.AutoReset = true;
            //_timer.Enabled = true;
            //_timer.Elapsed += SaveOrdersAsync;
            _orderStatus = JsonSerializer.Deserialize<Dictionary<string, string>>("{\"waiting_for_approval\": \"Onay Bekliyor\",\"approved\": \"Onaylandı\",\"fulfilled\": \"Kargoya Verildi\",\"cancelled\": \"İptal Edildi\",\"delivered\": \"Teslim Edildi\",\"on_accumulation\": \"Tedarik Sürecinde\",\"waiting_for_payment\": \"Ödeme Bekleniyor\",\"being_prepared\": \"Hazırlanıyor\",\"refunded\": \"İade Edildi\",\"personal_status_1\": \"Kişisel Sipariş Durumu 1\",\"personal_status_2\": \"Kişisel Sipariş Durumu 2\",\"personal_status_3\": \"Kişisel Sipariş Durumu 3\",\"deleted\": \"Silindi\"}");
        }
        public async void SaveOrdersAsync(/*Object source, ElapsedEventArgs e*/)
        {
            List<Order> baseOrders = new List<Order>();
            try
            {
                string lastTime = await _orderService.GetLastPullTimeAsync();
                //string lastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");    
                var orders = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>($"api/orders?startDate=" + lastTime);
                //var ordersUpdated = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>($"api/orders?startUpdatedAt=" + lastTime);
                //orders.AddRange(ordersUpdated);

                int i = 1;
                foreach (Core.DTOs.IdeaSoft.Order order in orders)
                {
                    var tmp = JsonSerializer.Deserialize<Dictionary<string, double[]>>(order.CurrencyRates);
                    Order baseOrder = new Order()
                    {
                        Amount = order.Amount,
                        CreatedAt = order.CreatedAt,
                        CurrTransaction = order.Currency,
                        CustomerName = order.CustomerFirstname,
                        CustomerSurName = order.CustomerSurname,
                        Number = order.TransactionId,
                        StoreName = "IdeaSoft",
                        StoreId = new Guid("b38e60bb-2bbe-4c7a-b47b-68eaabb7eeff"),
                        Integration = "",
                        TransferStatus = false,
                        AuxilCode = order.Source,
                        Date_ = order.CreatedAt,
                        Email = order.CustomerEmail,
                        PhoneNumber = order.CustomerPhone,
                        RcXrate = tmp["USD"] != null ? tmp["USD"][0] : 1,
                        TcXrate = tmp[order.Currency][0],
                        Status = _orderStatus[order.Status]
                    };

                    foreach (var item in order.OrderItems)
                    {
                        baseOrder.Transactions.Add(new OrderTransaction()
                        {
                            Name = item.ProductName,
                            OtherCode = item.ProductSku,
                            Quantity = item.ProductQuantity,
                            Order = baseOrder,
                            Price = item.ProductPrice,
                            TransDescription = order.TransactionId,
                            UnitCode = item.ProductStockTypeLabel,
                            UnitConv1 = 1,
                            UnitConv2 = item.PriceRatio,
                            CurrTrans = item.ProductCurrency,
                            TcXrate = tmp[item.ProductCurrency][0],
                            VatRate = item.ProductTax
                        });
                    }
                    baseOrders.Add(baseOrder);
                    Console.WriteLine(i.ToString() + ". Sipariş ID:" + order.TransactionId);
                    i++;
                }

                if (baseOrders.Count != 0)
                {
                    await _orderService.AddOrUpdateAsync(baseOrders);
                    Console.WriteLine("Siparişler Aktarıldı");
                    await _orderService.OrderLog(new OrderLog()
                    {
                        ImportedOrderCount = baseOrders.Count,
                        RunTime = DateTime.Now,
                        Status = true
                    });
                }
                else
                {
                    Console.WriteLine("Bu Tarihten İtibaren Oluşturulmuş Sipariş Bulunamadı:" + lastTime);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Siparişler Aktarılırken Hata Oluştur: " + ex.Message);
                Console.ReadKey();
                await _orderService.OrderLog(new OrderLog()
                {
                    ImportedOrderCount = baseOrders.Count,
                    RunTime = DateTime.Now,
                    Status = false,
                    Error = ex.Message
                });
                throw;
            }
        }
    }
}