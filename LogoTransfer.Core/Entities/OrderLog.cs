using System.ComponentModel.DataAnnotations;

namespace LogoTransfer.Core.Entities
{
    public class OrderLog
    {
        [Key]
        public Guid ProcessId { get; set; }
        public DateTime RunTime { get; set; }
        public bool Status { get; set; }
        public int ImportedOrderCount { get; set; }
        public string? Error { get; set; }
    }
}
