using LogoTransfer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogoTransfer.Repository.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                 new User()
                 {
                     Id = new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                     FirstName = "Super",
                     LastName = "User",
                     EMail = "admin@logo.com.tr",
                     UserName = "supervisor",
                     Password = "123",
                     RoleId = new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 }
             );
        }
    }
}
