using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Persistence.DbContexts
{
    public class OrderDbContext :  ApplicationDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> orderDbContext) : base(orderDbContext)
        {

        }
        public DbSet<Order.Domain.Orders.Order> Orders { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }


        public override void Dispose()
        {
            Console.WriteLine("OrderDbContext Dispose");
            Console.WriteLine(Environment.StackTrace);

            base.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            Console.WriteLine("OrderDbContext DisposeAsync");
            Console.WriteLine(Environment.StackTrace);

            await base.DisposeAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OrderDbContext).Assembly);
        }
    }
}
