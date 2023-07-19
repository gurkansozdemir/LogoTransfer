using LogoTransfer.Core.DTOs;
using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LogoTransfer.ImportService.Services
{
    public class IdeaSoftService
    {
        private readonly HttpClient _httpClientIdeasoft;
        private readonly HttpClient _httpClientLogo;
        private readonly IOrderService _orderService;
        private readonly Dictionary<string, string> _orderStatus;

        public IdeaSoftService(IOrderService orderService, IHttpClientFactory httpClient)
        {
            _httpClientIdeasoft = httpClient.CreateClient("IdeaSoftAPI");
            _httpClientLogo = httpClient.CreateClient("LogoTransferAPI");
            _orderService = orderService;
            _orderStatus = JsonSerializer.Deserialize<Dictionary<string, string>>("{\"waiting_for_approval\": \"Onay Bekliyor\",\"approved\": \"Onaylandı\",\"fulfilled\": \"Kargoya Verildi\",\"cancelled\": \"İptal Edildi\",\"delivered\": \"Teslim Edildi\",\"on_accumulation\": \"Tedarik Sürecinde\",\"waiting_for_payment\": \"Ödeme Bekleniyor\",\"being_prepared\": \"Hazırlanıyor\",\"refunded\": \"İade Edildi\",\"personal_status_1\": \"Kişisel Sipariş Durumu 1\",\"personal_status_2\": \"Kişisel Sipariş Durumu 2\",\"personal_status_3\": \"Kişisel Sipariş Durumu 3\",\"deleted\": \"Silindi\"}");
        }
        public async void SaveOrdersAsync()
        {
            List<Order> baseOrders = new List<Order>();
            try
            {
                var token = await _httpClientLogo.GetFromJsonAsync<CustomResponseDto<Token>>("authorization/getIdeaSoftTokenFromCache");

                _httpClientIdeasoft.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Data.Access_Token);
                string lastTime = await _orderService.GetLastPullTimeAsync();
                var orders = await _httpClientIdeasoft.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>($"api/orders?startDate=" + lastTime);

                int i = 1;
                foreach (Core.DTOs.IdeaSoft.Order order in orders)
                {
                    var tmp = JsonSerializer.Deserialize<Dictionary<string, double[]>>(order.CurrencyRates);
                    Order baseOrder = new Order()
                    {
                        Amount = order.GeneralAmount,
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
                        TaxNo = order.BillingAddress.TaxNo,
                        TaxOffice = order.BillingAddress.TaxOffice,
                        TckNumber = order.BillingAddress.IdentityRegistrationNumber,
                        Town = order.BillingAddress.SubLocation,
                        City = order.BillingAddress.Location,
                        Address = order.BillingAddress.Address,
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
                            Price = item.ProductPrice * (1 + (item.ProductTax / 100)),
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
                await _orderService.AutoImportAsync();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Siparişler Aktarılırken Hata Oluştur: " + ex.Message);
                
                await _orderService.OrderLog(new OrderLog()
                {
                    ImportedOrderCount = baseOrders.Count,
                    RunTime = DateTime.Now,
                    Status = false,
                    Error = ex.Message
                });
                throw;
            }
            System.Environment.Exit(1);
        }
    }
}