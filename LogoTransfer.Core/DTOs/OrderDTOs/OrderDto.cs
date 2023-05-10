using LogoTransfer.Core.DTOs.ProductDTOs;

namespace LogoTransfer.Core.DTOs.OrderDTOs
{
    public class OrderDto : BaseDto
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string OrderNo { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public List<ProductDto> Products { get; set; }
        public string TransferStatus { get; set; }
        public string Integration { get; set; }
    }
}
