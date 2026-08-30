using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Contracts.IntegrationEvents.Payments
{
    public sealed record PaymentFailedIntegrationEvent(
    Guid EventId,
    Guid OrderId,
    string Reason,
    DateTime OccurredOnUtc);
}
