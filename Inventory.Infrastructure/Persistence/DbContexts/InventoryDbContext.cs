using BuildingBlocks.Application.Messaging.Inbox;
using BuildingBlocks.Application.Messaging.Outbox;
using BuildingBlocks.Infrastructure.Persistence;
using Inventory.Domain.Aggregates.ProductInventory;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.DbContexts
{
    public class InventoryDbContext : ApplicationDbContext
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
