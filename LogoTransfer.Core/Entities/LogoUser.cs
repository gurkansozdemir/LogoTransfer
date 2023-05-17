namespace LogoTransfer.Core.Entities
{
    public class LogoUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirmCode { get; set; }
        public string PeriodNr { get; set; }
    }
}
