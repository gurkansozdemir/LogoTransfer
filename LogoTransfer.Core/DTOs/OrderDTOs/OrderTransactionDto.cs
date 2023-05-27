namespace LogoTransfer.Core.DTOs.OrderDTOs
{
    public class OrderTransactionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MasterCode { get; set; }
        public string OtherCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string TransDescription { get; set; }
        public string UnitCode { get; set; }
        public double UnitConv1 { get; set; }
        public double UnitConv2 { get; set; }
        public string CurrTrans { get; set; }
        public double TcXrate { get; set; }
        public double VatRate { get; set; }
        public Guid OrderId { get; set; }
        public bool IsProductMatch { get; set; }
    }
}
