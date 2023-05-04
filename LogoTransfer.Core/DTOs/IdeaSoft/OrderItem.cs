namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public object ProductBarcode { get; set; }
        public double ProductPrice { get; set; }
        public string ProductCurrency { get; set; }
        public double ProductQuantity { get; set; }
        public int ProductTax { get; set; }
        public double ProductDiscount { get; set; }
        public double ProductMoneyOrderDiscount { get; set; }
        public double ProductWeight { get; set; }
        public string ProductStockTypeLabel { get; set; }
        public int IsProductPromotioned { get; set; }
        public double Discount { get; set; }
        public double PriceRatio { get; set; }
        public object Product { get; set; }
        public List<object> OrderItemCustomizations { get; set; }
        public object OrderItemSubscription { get; set; }
    }
}
