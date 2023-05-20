using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogoTransfer.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public virtual Store Store { get; set; }
        [ForeignKey("StoreId")]
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        [Required]
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
        public List<OrderTransaction> Transactions { get; set; } = new List<OrderTransaction>();
        public string TransferStatus { get; set; }
        public string Integration { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Amount { get; set; }
    }
}
