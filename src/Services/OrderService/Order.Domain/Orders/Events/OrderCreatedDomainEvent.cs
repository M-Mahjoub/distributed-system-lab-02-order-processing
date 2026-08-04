using BuildingBlocks.Domain;

namespace Order.Domain.Orders.Events
{
    public sealed class OrderCreatedDomainEvent() : DomainEvent
    {
        public OrderId OrderId { get; set; }
        public CustomerId CustomerId { get; set; }
    }
}
