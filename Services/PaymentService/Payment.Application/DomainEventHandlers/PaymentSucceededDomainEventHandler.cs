using BuildingBlocks.Contracts.IntegrationEvents.Payments;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Persistence;
using Payment.Domain.Aggregates.Payment.Events;

namespace Payment.Application.DomainEventHandlers
{
    public sealed class PaymentSucceededDomainEventHandler
     : IDomainEventHandler<PaymentSucceededDomainEvent>
    {
        private readonly IIntegrationEventCollector _collector;

        public PaymentSucceededDomainEventHandler(
            IIntegrationEventCollector collector)
        {
            _collector = collector;
        }

        public Task HandleAsync(
            PaymentSucceededDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            _collector.Add(
                new PaymentSucceededIntegrationEvent(
                    domainEvent.EventId,
                    domainEvent.OccurredOnUtc,
                    domainEvent.OrderId));

            return Task.CompletedTask;
        }
    }
}
