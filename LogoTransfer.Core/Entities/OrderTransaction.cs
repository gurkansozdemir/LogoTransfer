using System.ComponentModel.DataAnnotations.Schema;

namespace LogoTransfer.Core.Entities
{
    public class OrderTransaction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OtherCode { get; set; }
        public string MasterCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string TransDescription { get; set; }
        public string UnitCode { get; set; }
        public double UnitConv1 { get; set; }
        public double UnitConv2 { get; set; }
        public string CurrTrans { get; set; }
        public double TcXrate { get; set; }
        public double VatRate { get; set; }

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public bool IsProductMatch { get; set; }
    }
}
