namespace LogoTransfer.Core.DTOs.ProductDTOs
{
    public class ProductDto : BaseDto
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal PriceRatio { get; set; }
        public string Currency { get; set; }
    }
}
