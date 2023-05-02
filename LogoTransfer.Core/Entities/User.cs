﻿namespace LogoTransfer.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}