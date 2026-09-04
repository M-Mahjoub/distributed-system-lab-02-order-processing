using BuildingBlocks.Contracts.IntegrationEvents;

namespace BuildingBlocks.Contracts.Inventory
{
    public sealed record CancelOrderIntegrationCommand(
      Guid EventId,
      Guid OrderId,
      DateTime OccurredOnUtc) : IntegrationEvent(EventId, OccurredOnUtc);
}
