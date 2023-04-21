namespace LogoTransfer.Web.Models.RoleModels
{
    public class MenuItemModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int RowNumber { get; set; }
        public string Icon { get; set; }
        public Guid RoleId { get; set; }
        public RoleModel Role { get; set; }
        public Guid? MainMenuItemId { get; set; }
        public MenuItemModel MainMenuItem { get; set; }
    }
}
