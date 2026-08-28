namespace Inventory.Infrastructure.Persistence.Configurations
{
    using global::Inventory.Domain.Aggregates.ProductInventory;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public sealed class ProductInventoryConfiguration
        : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(
            EntityTypeBuilder<ProductInventory> builder)
        {
            builder.ToTable("product_inventories");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Product
            builder.Property(x => x.ProductId)
                .IsRequired();

            // Quantities
            builder.Property(x => x.AvailableQuantity)
                .IsRequired();

            builder.Property(x => x.ReservedQuantity)
                .IsRequired();

            // One ProductInventory -> Many Reservations
            builder.HasMany(x => x.Reservations)
                .WithOne()
                .HasForeignKey("ProductInventoryId")
                .OnDelete(DeleteBehavior.Cascade);

            // EF Core should use the private backing field
            // to access the collection.
            builder.Metadata
                .FindNavigation(nameof(ProductInventory.Reservations))!
                .SetPropertyAccessMode(
                    PropertyAccessMode.Field);
        }
    }
}
