using LogoTransfer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogoTransfer.Repository.Seeds
{
    internal class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role()
                {
                    Id = new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                    Name = "Supervisor",
                    Description = "Full Authorize",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                },

                new Role()
                {
                    Id = new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                    Name = "StandartUser",
                    Description = "Default User",
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}
