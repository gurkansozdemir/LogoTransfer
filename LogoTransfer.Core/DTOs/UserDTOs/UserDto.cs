namespace LogoTransfer.Core.DTOs.UserDTOs
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
