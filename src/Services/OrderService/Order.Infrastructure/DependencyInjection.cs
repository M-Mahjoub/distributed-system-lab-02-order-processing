using BuildingBlocks.Application.Abstractions.DomainEvents;
using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Messaging.Inbox;
using BuildingBlocks.Application.Messaging.Outbox;
using BuildingBlocks.Contracts.IntegrationEvents;
using BuildingBlocks.Contracts.Inventory;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Messaging.DomainEvents;
using BuildingBlocks.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Persistence.Transactions;
using Inventory.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstractions.Persistence;
using Order.Application.Messaging;
using Order.Domain.Abnstractions;
using Order.Domain.Orders.Events;
using Order.Infrastructure.BackgroundServices;
using Order.Infrastructure.DomainEventHandlers;
using Order.Infrastructure.Messaging;
using Order.Infrastructure.Messaging.EventBuses;
using Order.Infrastructure.Messaging.IntegrationEvents;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Persistence.DbContexts;
using Order.Infrastructure.Persistence.Outbox;
using Order.Infrastructure.Persistence.Repositories;
using System.Text.Json;

namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureDI(this IServiceCollection serviceDescriptors,
                                                              IConfigurationManager configurationManager)
        {
            serviceDescriptors.AddDbContext<OrderDbContext>(cfg =>
                               cfg.UseNpgsql(configurationManager.GetConnectionString("OrderDb")));


            serviceDescriptors.Configure<RabbitMqOptions>(
                configurationManager.GetSection("RabbitMq"));

            serviceDescriptors.AddSingleton<IEventBus, RabbitMqEventBus>();

            serviceDescriptors.AddScoped<IOrderRepository, OrderRepository>();
            serviceDescriptors.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            serviceDescriptors.AddSingleton(_ => new JsonSerializerOptions());
            serviceDescriptors.AddScoped<IOutboxMessageFactory, SystemTextJsonOutboxMessageFactory>();
            serviceDescriptors.AddScoped<IIntegrationEventCollector, IntegrationEventCollector>();
            serviceDescriptors.AddScoped(typeof(DomainEventHandlerExecutor<>));
            serviceDescriptors.AddScoped<
                                        IDomainEventHandler<OrderCreatedDomainEvent>,
                                        OrderCreatedDomainEventHandler>();
            serviceDescriptors.AddScoped<IUnitOfWork, EfUnitOfWork<OrderDbContext>>();
            serviceDescriptors.AddHostedService<OutboxProcessorBackgroundService>();
            serviceDescriptors.AddSingleton<IIntegrationEventTypeRegistry, IntegrationEventTypeRegistry>();
            serviceDescriptors.AddScoped<IIntegrationEventCollector, IntegrationEventCollector>();

            serviceDescriptors.AddScoped<
                               IOrderSagaRepository,
                               OrderSagaRepository>();

            serviceDescriptors.AddScoped<
                               ITransactionManager,
                               EfTransactionManager<OrderDbContext>>();
                               
            serviceDescriptors.AddScoped<
                               IInboxRepository,
                               InboxRepository>();

            //serviceDescriptors.AddScoped<
            //    IMessageHandler<InventoryReleasedForOrderIntegrationEvent>,
            //    InventoryReleasedMessageHandler>();

            serviceDescriptors.AddScoped<
                              IMessageIdAccessor<InventoryReleasedForOrderIntegrationEvent>,
                              InventoryReleasedEventIdAccessor>();


            serviceDescriptors.AddScoped<
                               ITransactionalMessageHandler<
                                   InventoryReleasedForOrderIntegrationEvent>>(
            provider =>
    {
        var transactionManager =
            provider.GetRequiredService<ITransactionManager>();

        var inboxRepository =
            provider.GetRequiredService<IInboxRepository>();

        var handler =
            provider.GetRequiredService<
                IMessageHandler<
                    InventoryReleasedForOrderIntegrationEvent>>();

        var idAccessor =
            provider.GetRequiredService<
                IMessageIdAccessor<
                    InventoryReleasedForOrderIntegrationEvent>>();

        return new TransactionalMessageHandler<
            InventoryReleasedForOrderIntegrationEvent>(
            transactionManager,
            inboxRepository,
            handler,
            idAccessor,
            "Order.InventoryReleasedForOrderConsumer");
    });
            
                    return serviceDescriptors;
        }
    }
}
