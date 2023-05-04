namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class OrderItem
    {
        public int id { get; set; }
        public string productName { get; set; }
        public string productSku { get; set; }
        public object productBarcode { get; set; }
        public double productPrice { get; set; }
        public string productCurrency { get; set; }
        public double productQuantity { get; set; }
        public int productTax { get; set; }
        public double productDiscount { get; set; }
        public double productMoneyOrderDiscount { get; set; }
        public double productWeight { get; set; }
        public string productStockTypeLabel { get; set; }
        public int isProductPromotioned { get; set; }
        public double discount { get; set; }
        public double priceRatio { get; set; }
        public object product { get; set; }
        public List<object> orderItemCustomizations { get; set; }
        public object orderItemSubscription { get; set; }
    }
}
