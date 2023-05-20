namespace LogoTransfer.Core.Entities
{
    public class ProductMatching : BaseEntity
    {
        public string MasterProductCode { get; set; }
        public string OtherProductCode { get; set; }
    }
}
