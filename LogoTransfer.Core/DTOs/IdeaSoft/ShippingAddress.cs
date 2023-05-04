namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class ShippingAddress
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string country { get; set; }
        public string location { get; set; }
        public string subLocation { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string mobilePhoneNumber { get; set; }
        public object zipCode { get; set; }
    }
}
