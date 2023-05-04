namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public object ZipCode { get; set; }
    }
}
