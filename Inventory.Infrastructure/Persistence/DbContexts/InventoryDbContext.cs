using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.DbContexts
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Domain.Aggregates.Inventory.Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
        }
    }
}
