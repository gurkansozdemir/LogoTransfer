namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class Maillist
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public MaillistGroup maillistGroup { get; set; }
    }
}
