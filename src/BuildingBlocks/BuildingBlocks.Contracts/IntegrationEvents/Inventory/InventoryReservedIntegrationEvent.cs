using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Contracts.IntegrationEvents.Inventory
{
    public sealed record InventoryReservedIntegrationEvent(
    Guid EventId,
    DateTime OccurredOnUtc,
    Guid OrderId) : IntegrationEvent(EventId, OccurredOnUtc)
    {
    }
}
