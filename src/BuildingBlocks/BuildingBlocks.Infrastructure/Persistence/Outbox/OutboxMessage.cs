namespace BuildingBlocks.Infrastructure.Persistence.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }

        public string Type { get; private set; } = default!;

        public string Payload { get; private set; } = default!;

        //order.created
        public string? RoutingKey { get; init; }

        public DateTime OccurredOnUtc { get; private set; }

        public DateTime? ProcessedOnUtc { get; private set; }

        public int RetryCount { get; private set; }

        public string? Error { get; private set; }

        private OutboxMessage()
        {
        }

        public OutboxMessage(
            Guid id,
            string type,
            string payload,
            DateTime occurredOnUtc)
        {
            Id = id;
            Type = type;
            Payload = payload;
            OccurredOnUtc = occurredOnUtc;
        }

        public void MarkAsProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;
        }

        public void MarkAsFailed(string error)
        {
            RetryCount++;
            Error = error;
        }
    }
}
