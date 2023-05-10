﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LogoTransfer.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal PriceRatio { get; set; }
        public string Currency { get; set; }

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
