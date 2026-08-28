using BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Aggregates.Payment.Events
{
    public sealed record PaymentFailedDomainEvent(
     Guid EventId,
     DateTime OccurredOnUtc,
     Guid OrderId)
     : IDomainEvent;
}
