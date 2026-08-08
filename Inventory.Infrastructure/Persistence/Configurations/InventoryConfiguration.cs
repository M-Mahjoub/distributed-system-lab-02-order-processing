using Inventory.Domain.Aggregates.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Inventory.Inventory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Aggregates.Inventory.Inventory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id)
                .HasConversion(
                id => id.Value,
                value => new InventoryId(value));

            //با  این خط: EF از این لحظه SQL را این شکلی تولید می‌کند: UPDATE Inventory
            //SET AvailableQuantity = ...
            //WHERE
            //    Id = @Id
            //AND
            //    Version = @Version
            builder.Property(x => x.Version)
               .IsRowVersion();
        }
    }
}
