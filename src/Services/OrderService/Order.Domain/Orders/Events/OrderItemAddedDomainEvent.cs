using BuildingBlocks.Domain.Events;
using Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Orders.Events
{
    public sealed record OrderItemAddedDomainEvent(ProductSnapshot Product, int quantity) : DomainEvent
    {
    }
}
