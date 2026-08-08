using BuildingBlocks.Domain.Events;

namespace Order.Domain.Orders.Events
{
    public sealed record OrderCreatedDomainEvent() : DomainEvent
    {
        public OrderId OrderId { get; set; }
        public CustomerId CustomerId { get; set; }
    }
}
