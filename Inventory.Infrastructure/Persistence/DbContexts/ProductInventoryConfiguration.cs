using Inventory.Domain.Aggregates.ProductInventory;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Persistence.DbContexts
{
    public class ProductInventoryConfiguration
     : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(
            EntityTypeBuilder<ProductInventory> builder)
        {
            builder.ToTable("product_inventories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.AvailableQuantity)
                .IsRequired();

            builder.Property(x => x.ReservedQuantity)
                .IsRequired();
        }
    }
}
