namespace BuildingBlocks.Contracts.IntegrationEvents
{
    public abstract record IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; init; } = Guid.CreateVersion7();

        public DateTime OccurredOnUtc { get; init; }
            = DateTime.UtcNow;

        public int Version { get; init; } = 1;
    }
}
