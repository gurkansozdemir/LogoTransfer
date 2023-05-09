namespace LogoTransfer.Core.Entities
{
    public class Product : BaseEntity
    {
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

    }
}
