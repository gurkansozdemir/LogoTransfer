namespace LogoTransfer.Core.DTOs.IdeaSoft
{
    public class Maillist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public MaillistGroup MaillistGroup { get; set; }
    }
}
