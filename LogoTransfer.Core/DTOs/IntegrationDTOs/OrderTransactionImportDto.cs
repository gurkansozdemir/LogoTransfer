using System.Text.Json.Serialization;

namespace LogoTransfer.Core.DTOs.IntegrationDTOs
{
    public class OrderTransactionImportDto
    {
        public string MasterCode { get; set; }
        [JsonIgnore]
        public string? OtherCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string TransDescription { get; set; }
        public string UnitCode { get; set; }
        public double UnitConv1 { get; set; }
        public double UnitConv2 { get; set; }
        public string CurrTrans { get; set; }
        public double TcXrate { get; set; }
        public double VatRate { get; set; }
        [JsonIgnore]
        public bool IsProductMatch { get; set; }
    }
}
