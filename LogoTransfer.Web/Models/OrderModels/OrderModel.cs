namespace LogoTransfer.Web.Models.OrderModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentProviderCode { get; set; }
        public string PaymentProviderName { get; set; }
        public string PaymentGatewayCode { get; set; }
        public string PaymentGatewayName { get; set; }
        public string BankName { get; set; }
        public string ClientIp { get; set; }
        public string UserAgent { get; set; }
        public string Currency { get; set; }
        public string CurrencyRates { get; set; }
        public double Amount { get; set; }
        public double CouponDiscount { get; set; }
        public double TaxAmount { get; set; }
        public double TotalCustomTaxAmount { get; set; }
        public double PromotionDiscount { get; set; }
        public double GeneralAmount { get; set; }
        public double ShippingAmount { get; set; }
        public double AdditionalServiceAmount { get; set; }
        public double FinalAmount { get; set; }
        public double SumOfGainedPoints { get; set; }
        public int Installment { get; set; }
        public double InstallmentRate { get; set; }
        public int ExtraInstallment { get; set; }
        public string TransactionId { get; set; }
        public int HasUserNote { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public object ErrorMessage { get; set; }
        public string DeviceType { get; set; }
        public object Referrer { get; set; }
        public int InvoicePrintCount { get; set; }
        public int UseGiftPackage { get; set; }
        public object GiftNote { get; set; }
        public string MemberGroupName { get; set; }
        public int UsePromotion { get; set; }
        public string ShippingProviderCode { get; set; }
        public string ShippingProviderName { get; set; }
        public string ShippingCompanyName { get; set; }
        public string ShippingPaymentType { get; set; }
        public object ShippingTrackingCode { get; set; }
        public string Source { get; set; }
    }
}
