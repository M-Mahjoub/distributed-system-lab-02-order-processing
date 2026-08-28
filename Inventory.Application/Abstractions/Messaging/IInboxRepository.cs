using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Abstractions.Messaging
{
    public interface IInboxRepository
    {
        Task<bool> ExistsAsync(
            Guid messageId,
            CancellationToken cancellationToken);

        Task AddAsync(
            Guid messageId,
            string type,
            DateTime receivedOnUtc,
            CancellationToken cancellationToken);

        Task MarkProcessedAsync(
            Guid messageId,
            DateTime processedOnUtc,
            CancellationToken cancellationToken);
    }
}
