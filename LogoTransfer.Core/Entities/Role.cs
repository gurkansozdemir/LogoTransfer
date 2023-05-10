namespace LogoTransfer.Core.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }

    }
}
