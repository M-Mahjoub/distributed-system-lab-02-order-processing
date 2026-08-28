//using BuildingBlocks.Domain;
//using BuildingBlocks.Infrastructure.Persistence;
//using Payment.Domain.Aggregates.Payment.Events;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Payment.Application.DomainEventHandlers
//{
//    public sealed class PaymentFailedDomainEventHandler
//    : IDomainEventHandler<PaymentFailedDomainEvent>
//    {
//        private readonly IIntegrationEventCollector _collector;

//        public PaymentFailedDomainEventHandler(
//            IIntegrationEventCollector collector)
//        {
//            _collector = collector;
//        }

//        public Task HandleAsync(
//            PaymentFailedDomainEvent domainEvent,
//            CancellationToken cancellationToken)
//        {
//            _collector.Add(
//                new PaymentFailedIntegrationEvent(
//                    domainEvent.EventId,
//                    domainEvent.OccurredOnUtc,
//                    domainEvent.OrderId));

//            return Task.CompletedTask;
//        }
//    }
//}
