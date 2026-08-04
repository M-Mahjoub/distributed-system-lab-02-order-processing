using Order.Domain.Orders;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.IntegrationEvents
{
    public sealed record OrderCreatedIntegrationEvent(OrderId OrderId, CustomerId CustomerId) : IntegrationEvent
    {
    }
}
