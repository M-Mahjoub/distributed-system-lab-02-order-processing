namespace BuildingBlocks.Contracts.IntegrationEvents
{
    public abstract record IntegrationEvent : IIntegrationEvent
    {
        protected IntegrationEvent(
        Guid eventId,
        DateTime occurredOnUtc,
        int version = 1)
        {
            EventId = eventId;
            OccurredOnUtc = occurredOnUtc;
            Version = version;
        }
        public Guid EventId { get; init; } = Guid.CreateVersion7();

        public DateTime OccurredOnUtc { get; init; }
            = DateTime.UtcNow;

        public int Version { get; init; } = 1;
    }
}
