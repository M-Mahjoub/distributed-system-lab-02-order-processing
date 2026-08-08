using BuildingBlocks.Domain.Events;

namespace Order.Domain.Orders.Events
{
    public record OrderConfirmedDomainEvent : DomainEvent
    {
        public OrderId OrderId { get; set; }
        public CustomerId CustomerId { get; set; }
    }
}
