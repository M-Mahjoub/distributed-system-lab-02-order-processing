using BuildingBlocks.Application.Abstractions.DomainEvents;
using BuildingBlocks.Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Infrastructure.Messaging.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public IDomainEventHandlerExecutor _domainEventHandlerExecutor { get; set; }

        private readonly IPublisher _publisher;

        public DomainEventDispatcher(IServiceProvider serviceProvider,
            IPublisher publisher)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;
        }
        public async Task DispatchAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {

            foreach (var domainEvent in domainEvents)
            {
                var executorType = typeof(DomainEventHandlerExecutor<>)
                                         .MakeGenericType(domainEvent.GetType());
                try
                {


                    _domainEventHandlerExecutor = (IDomainEventHandlerExecutor)
                       _serviceProvider.GetRequiredService(executorType);

                }
                catch (Exception ex)
                {

                    throw;
                }

                await _domainEventHandlerExecutor.Execute(domainEvent, cancellationToken);

                // await _publisher.Publish(
                //new DomainEventNotification(domainEvent),
                //cancellationToken);

            }
        }
    }
}
