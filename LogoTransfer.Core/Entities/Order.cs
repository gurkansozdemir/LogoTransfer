namespace LogoTransfer.Core.Entities
{
    public class Order : BaseEntity
    {
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string OrderNo { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public List<Product> Products { get; set; }
        public string TransferStatus { get; set; }
        public string Integration { get; set; }
    }
}
