using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;
using Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order.Domain.Orders.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Orders.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(
                id => id.Value,
                value => new OrderId(value));

            builder.Property(x => x.CustomerId)
                  .HasConversion(
                      id => id.Value,
                      value => new CustomerId(value));

            builder.Property(x => x.Status)
                  .HasConversion<string>();


        }
    }
}
