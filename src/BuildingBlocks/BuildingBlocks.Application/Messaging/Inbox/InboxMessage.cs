namespace BuildingBlocks.Application.Messaging.Inbox
{
    public sealed class InboxMessage
    {
        public Guid Id { get; private set; }

        //چرا Consumer هم داریم؟
        //فرض کن Message: به دو Consumer برسد: هر دو باید بتوانند همان MessageId را مستقل پردازش کنند.
        public string Consumer { get; private set; } = null!;

        public DateTime ReceivedOnUtc { get; private set; }

        public DateTime? ProcessedOnUtc { get; private set; }

        public int RetryCount { get; private set; }

        public string? Error { get; private set; }

        private InboxMessage()
        {
        }

        public InboxMessage(
            Guid id,
            string consumer,
            DateTime receivedOnUtc)
        {
            Id = id;
            Consumer = consumer;
            ReceivedOnUtc = receivedOnUtc;
        }

        public void MarkAsProcessed(
            DateTime processedOnUtc)
        {
            ProcessedOnUtc = processedOnUtc;
            Error = null;
        }

        public void MarkAsFailed(string error)
        {
            RetryCount++;
            Error = error;
        }
    }
}
