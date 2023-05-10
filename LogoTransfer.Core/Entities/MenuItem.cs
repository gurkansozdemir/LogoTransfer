using System.ComponentModel.DataAnnotations.Schema;

namespace LogoTransfer.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int RowNumber { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        [ForeignKey("MainMenuItemId")]
        public Guid? MainMenuItemId { get; set; }
        public virtual MenuItem MainMenuItem { get; set; }
    }
}
