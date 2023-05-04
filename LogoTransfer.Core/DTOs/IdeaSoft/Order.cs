namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class Order
    {
        public int id { get; set; }
        public string customerFirstname { get; set; }
        public string customerSurname { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string paymentTypeName { get; set; }
        public string paymentProviderCode { get; set; }
        public string paymentProviderName { get; set; }
        public string paymentGatewayCode { get; set; }
        public string paymentGatewayName { get; set; }
        public string bankName { get; set; }
        public string clientIp { get; set; }
        public string userAgent { get; set; }
        public string currency { get; set; }
        public string currencyRates { get; set; }
        public double amount { get; set; }
        public double couponDiscount { get; set; }
        public double taxAmount { get; set; }
        public double totalCustomTaxAmount { get; set; }
        public double promotionDiscount { get; set; }
        public double generalAmount { get; set; }
        public double shippingAmount { get; set; }
        public double additionalServiceAmount { get; set; }
        public double finalAmount { get; set; }
        public double sumOfGainedPoints { get; set; }
        public int installment { get; set; }
        public double installmentRate { get; set; }
        public int extraInstallment { get; set; }
        public string transactionId { get; set; }
        public int hasUserNote { get; set; }
        public string status { get; set; }
        public string paymentStatus { get; set; }
        public object errorMessage { get; set; }
        public string deviceType { get; set; }
        public object referrer { get; set; }
        public int invoicePrintCount { get; set; }
        public int useGiftPackage { get; set; }
        public object giftNote { get; set; }
        public string memberGroupName { get; set; }
        public int usePromotion { get; set; }
        public string shippingProviderCode { get; set; }
        public string shippingProviderName { get; set; }
        public string shippingCompanyName { get; set; }
        public string shippingPaymentType { get; set; }
        public object shippingTrackingCode { get; set; }
        public string source { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public Maillist maillist { get; set; }
        public object member { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public List<object> orderCustomTaxLines { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public BillingAddress billingAddress { get; set; }
    }
}
