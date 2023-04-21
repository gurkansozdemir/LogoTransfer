using LogoTransfer.Web.Models.RoleModels;

namespace LogoTransfer.Web.Models.UserModels
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public RoleModel Role { get; set; }
    }
}
