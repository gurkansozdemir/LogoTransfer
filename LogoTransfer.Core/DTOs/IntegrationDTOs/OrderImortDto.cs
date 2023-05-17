using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoTransfer.Core.DTOs.IntegrationDTOs
{
    public class OrderImportDto
    {
        public string Number { get; set; }
        public DateTime Date_ { get; set; }
        public string AuxilCode { get; set; }
        public string ArpCode { get; set; }
        public double RcXrate { get; set; }
        public int CurrTransaction { get; set; }
        public double TcXrate { get; set; }
        public List<OrderTransactionImportDto> Transactions { get; set; }
    }
}
