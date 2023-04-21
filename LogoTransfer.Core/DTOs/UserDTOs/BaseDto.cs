namespace LogoTransfer.Core.DTOs.UserDTOs
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
    }
}
