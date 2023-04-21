namespace LogoTransfer.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int RowNumber { get; set; }
        public string Icon { get; set; }
        public ICollection<Role> Roles { get; set; }
        public Guid? MainMenuItemId { get; set; }
        public MenuItem MainMenuItem { get; set; }
    }
}
