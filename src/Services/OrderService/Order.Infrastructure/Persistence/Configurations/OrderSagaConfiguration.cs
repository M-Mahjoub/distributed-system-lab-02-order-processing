using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Sagas;

namespace Order.Infrastructure.Persistence.Configurations
{
    public sealed class OrderSagaConfiguration
        : IEntityTypeConfiguration<OrderSaga>
    {
        public void Configure(
            EntityTypeBuilder<OrderSaga> builder)
        {
            builder.ToTable("order_sagas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired();

            //چون برای هر Order فقط یک Saga Instance می‌خواهیم.
            builder.HasIndex(x => x.OrderId)
                .IsUnique();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.InventoryReserved)
                .IsRequired();

            builder.Property(x => x.PaymentSucceeded)
                .IsRequired();

            builder.Property(x => x.PaymentFailed)
                .IsRequired();

            builder.Property(x => x.InventoryReleased)
                .IsRequired();

            builder.Property(x => x.OrderCancelled)
                .IsRequired();
        }
    }
}
