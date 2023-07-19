namespace LogoTransfer.Core.DTOs.IntegrationDTOs
{
    public class OrderImportDto
    {
        public string Number { get; set; }
        public DateTime Date_ { get; set; }
        public string AuxilCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public double RcXrate { get; set; }
        public string CurrTransaction { get; set; }
        public double TcXrate { get; set; }
        public string? TaxNo { get; set; }
        public string? TaxOffice { get; set; }
        public string? TckNumber { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
        public List<OrderTransactionImportDto> Transactions { get; set; } = new List<OrderTransactionImportDto>();
    }
}
