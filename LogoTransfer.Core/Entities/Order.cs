﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LogoTransfer.Core.Entities
{
    public class Order : BaseEntity
    {
        public virtual Store Store { get; set; }
        [ForeignKey("StoreId")]
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string OrderNo { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public virtual List<Product> Products { get; set; }
        public string TransferStatus { get; set; }
        public string Integration { get; set; }
    }
}
