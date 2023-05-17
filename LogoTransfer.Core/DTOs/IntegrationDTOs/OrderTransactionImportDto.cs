using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoTransfer.Core.DTOs.IntegrationDTOs
{
    public class OrderTransactionImportDto
    {
        public string MasterCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string TransDescripntion { get; set; }
        public string UnitCode { get; set; }
        public double UnitConv1 { get; set; }
        public double UnitConv2 { get; set; }
        public int CurrTrans { get; set; }
        public double TcXrate { get; set; }
    }
}
