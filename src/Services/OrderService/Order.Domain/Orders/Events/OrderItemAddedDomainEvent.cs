using BuildingBlocks.Domain;
using Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Orders.Events
{
    public sealed class OrderItemAddedDomainEvent(ProductSnapshot Product, int quantity) : DomainEvent
    {
    }
}
