using LogoTransfer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LogoTransfer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<LogoUser> LogoUsers { get; set; }
        public DbSet<ProductMatching> ProductMatchings { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasIndex(u => u.Number).IsUnique();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
