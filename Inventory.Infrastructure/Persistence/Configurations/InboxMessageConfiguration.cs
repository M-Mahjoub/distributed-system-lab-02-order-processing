using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Application.Messaging.Inbox;

namespace Inventory.Infrastructure.Persistence.Configurations
{
    public sealed class InboxMessageConfiguration
     : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(
            EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("inbox_messages");

            builder.HasKey(x => new
            {
                x.Id,
                x.Consumer
            });

            builder.Property(x => x.Consumer)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.ReceivedOnUtc)
           .IsRequired();

            builder.Property(x => x.ProcessedOnUtc);

            builder.Property(x => x.RetryCount)
                .IsRequired();

            builder.Property(x => x.Error);

            //            دو Consumer همزمان می‌توانند این کار را بکنند:

            //Consumer A                  Consumer B

            //Exists(X) → false           Exists(X) → false
            //     ↓                           ↓
            //Process Process
            //     ↓                           ↓
            //Insert X                    Insert X

            //پس باید Database Unique Constraint هم داشته باشیم.

            builder.HasIndex(x => new
            {
                x.Id,
                x.Consumer
            })
            .IsUnique();
        }
    }
}
