namespace LogoTransfer.Core.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string OrderNo { get; set; }
        public string Number { get; set; }
        public string? Status { get; set; }
        public DateTime Date_ { get; set; }
        public string AuxilCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public double RcXrate { get; set; }
        public string CurrTransaction { get; set; }
        public double TcXrate { get; set; }
        public List<OrderTransactionDto> Transactions { get; set; }
        public bool TransferStatus { get; set; }
        public string Integration { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Amount { get; set; }
    }
}
