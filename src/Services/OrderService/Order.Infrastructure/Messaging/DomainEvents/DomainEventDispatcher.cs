using BuildingBlocks.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public IDomainEventHandlerExecutor _domainEventHandlerExecutor { get; set; }

        public DomainEventDispatcher(IServiceProvider serviceProvider
                                      )
        {
            _serviceProvider = serviceProvider;
            //_domainEventHandlerExecutor = domainEventHandlerExecutor;
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

                //var handlerType =
                //                typeof(IDomainEventHandler<>)
                //               .MakeGenericType(domainEvent.GetType());
                //var handlers =
                //             _serviceProvider.GetServices(handlerType);

                //foreach (var handler in handlers)
                //{

                await _domainEventHandlerExecutor.Execute(domainEvent, cancellationToken);
                //}
            }
        }
    }
}
