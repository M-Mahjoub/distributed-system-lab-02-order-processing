namespace BuildingBlocks.Contracts.Inventory
{
    public sealed record ReleaseInventoryIntegrationCommand(
      Guid MessageId,
      Guid OrderId);
}
