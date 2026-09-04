using BuildingBlocks.Contracts.IntegrationEvents;

namespace BuildingBlocks.Contracts.Inventory
{
    //این Event می‌گوید:

    //تمام Reservationهای مربوط به این Order در Inventory Service با موفقیت آزاد شدند.
    public sealed record InventoryReleasedForOrderIntegrationEvent(
     Guid EventId,
     Guid OrderId,
     DateTime OccurredOnUtc) : IntegrationEvent(EventId, OccurredOnUtc);
}
