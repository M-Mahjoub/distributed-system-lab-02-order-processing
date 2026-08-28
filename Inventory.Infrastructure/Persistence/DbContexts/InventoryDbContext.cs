using BuildingBlocks.Infrastructure.Persistence.Inbox;
using BuildingBlocks.Infrastructure.Persistence.Outbox;
using Inventory.Domain.Aggregates.ProductInventory;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.DbContexts
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<InboxMessage> InboxMessages =>
        Set<InboxMessage>();

        public DbSet<OutboxMessage> OutboxMessages =>
        Set<OutboxMessage>();
        public DbSet<ProductInventory> ProductInventories =>
        Set<ProductInventory>();

        public DbSet<Reservation> Reservations =>
       Set<Reservation>();

        public InventoryDbContext(DbContextOptions<InventoryDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Domain.Aggregates.Inventory.Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
