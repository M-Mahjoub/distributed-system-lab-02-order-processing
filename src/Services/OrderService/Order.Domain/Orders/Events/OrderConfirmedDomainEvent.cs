using BuildingBlocks.Domain;

namespace Order.Domain.Orders.Events
{
    public class OrderConfirmedDomainEvent : DomainEvent
    {
        public OrderId OrderId { get; set; }
        public CustomerId CustomerId { get; set; }
    }
}
