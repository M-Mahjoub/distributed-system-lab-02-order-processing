using Inventory.Domain.Aggregates.ProductInventory;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration
     : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(
            EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("inventory_reservations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
