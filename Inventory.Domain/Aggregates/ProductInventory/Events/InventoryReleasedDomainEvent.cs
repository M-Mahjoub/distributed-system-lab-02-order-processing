using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Aggregates.ProductInventory.Events
{
    public sealed record InventoryReleasedDomainEvent(
     Guid EventId,
     DateTime OccurredOnUtc,
     Guid OrderId,
     Guid ProductId,
     int Quantity)
     : IDomainEvent;
}
