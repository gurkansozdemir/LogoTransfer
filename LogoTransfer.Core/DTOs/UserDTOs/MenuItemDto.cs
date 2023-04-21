namespace LogoTransfer.Core.DTOs.RoleDTOs
{
    public class MenuItemDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int RowNumber { get; set; }
        public string Icon { get; set; }
        public Guid? MainMenuItemId { get; set; }
        public MenuItemDto MainMenuItem { get; set; }
    }
}
