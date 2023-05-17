using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoTransfer.Core.DTOs.IntegrationDTOs
{
    public class OrderImportResponseDto
    {
        public string Number { get; set; }
        public string ReturnNumber { get; set; }
        public string ReturnError { get; set; }
    }
}
