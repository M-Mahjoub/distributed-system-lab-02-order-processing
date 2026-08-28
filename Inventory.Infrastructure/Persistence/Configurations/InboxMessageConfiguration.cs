using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Persistence.Inbox;
using System.Data;

namespace Inventory.Infrastructure.Persistence.Configurations
{
    public sealed class InboxMessageConfiguration
     : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(
            EntityTypeBuilder<InboxMessage> builder)
        {
            builder.HasKey(x => x.MessageId);

            builder.Property(x => x.Type)
                .HasMaxLength(500)
                .IsRequired();

            //            دو Consumer همزمان می‌توانند این کار را بکنند:

            //Consumer A                  Consumer B

            //Exists(X) → false           Exists(X) → false
            //     ↓                           ↓
            //Process Process
            //     ↓                           ↓
            //Insert X                    Insert X

            //پس باید Database Unique Constraint هم داشته باشیم.
            builder.HasIndex(x => x.MessageId)
                .IsUnique();
        }
    }
}
