﻿using LogoTransfer.Core.Entities;
using LogoTransfer.Core.Services;
using LogoTransfer.Service.Caching;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Timers;

namespace LogoTransfer.ImportService.Services
{
    public class IdeaSoftService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;
        private readonly CacheDataImportService _cacheData;
        private static System.Timers.Timer _timer;

        public IdeaSoftService(IOrderService orderService, IHttpClientFactory httpClient, CacheDataImportService cacheData)
        {

            _httpClient = httpClient.CreateClient("IdeaSoftAPI");
            _orderService = orderService;
            _cacheData = cacheData;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cacheData.Token);
            _timer = new System.Timers.Timer(600000);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += SaveOrdersAsync;
        }
        public async void SaveOrdersAsync(Object source, ElapsedEventArgs e)
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Core.DTOs.IdeaSoft.Order>>("api/orders");
            List<Order> baseOrders = new List<Order>();

            foreach (Core.DTOs.IdeaSoft.Order order in orders)
            {
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
                    TransferStatus = "",
                    AuxilCode = order.Source,
                    Date_ = order.CreatedAt,
                    Email = order.CustomerEmail,
                    PhoneNumber = order.CustomerPhone,
                    RcXrate = 18.86, // currencyRates içeriisndeki USD değeri
                    TcXrate = 1 // currency TL ise 1 yoksa currencyRates içerisinde yazan değer
                };

                foreach (var item in order.OrderItems)
                {
                    baseOrder.Transactions.Add(new OrderTransaction()
                    {
                        Name = item.ProductName,
                        MasterCode = item.ProductSku, // orderItems.productSku da yazan malzeme kodunun logo da eşdeğer kodu
                        Quantity = item.ProductQuantity,
                        Order = baseOrder,
                        Price = item.ProductPrice,
                        TransDescription = order.TransactionId,
                        UnitCode = item.ProductStockTypeLabel,
                        UnitConv1 = 1,
                        UnitConv2 = item.PriceRatio,
                        CurrTrans = item.ProductCurrency,
                        TcXrate = 1, // currency TL ise 1 yoksa currencyRates içerisinde yazan değer
                        VatRate = item.ProductTax
                    });
                }
                baseOrders.Add(baseOrder);
            }

            await _orderService.AddRangeAsync(baseOrders);
        }
    }
}