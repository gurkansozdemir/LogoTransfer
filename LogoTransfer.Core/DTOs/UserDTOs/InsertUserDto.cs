namespace LogoTransfer.Core.DTOs.UserDTOs
{
    public class InsertUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public Guid RoleId { get; set; }
    }
}
