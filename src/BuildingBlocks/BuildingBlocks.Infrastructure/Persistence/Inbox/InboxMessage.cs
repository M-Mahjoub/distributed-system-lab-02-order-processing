using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.Persistence.Inbox
{
    public sealed class InboxMessage
    {
        public Guid MessageId { get; init; }

        public string Type { get; init; } = default!;

        public DateTime ReceivedOnUtc { get; init; }

        public DateTime? ProcessedOnUtc { get; set; }

        public int RetryCount { get; set; }

        public string? Error { get; set; }
    }
}
