using BuildingBlocks.Application;
using BuildingBlocks.Application.Abstractions.DomainEvents;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Messaging.DomainEvents;
using BuildingBlocks.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Abnstractions;
using Order.Domain.Orders.Events;
using Order.Infrastructure.BackgroundServices;
using Order.Infrastructure.DomainEventHandlers;
using Order.Infrastructure.IntegrationEvents;
using Order.Infrastructure.Messaging.IntegrationEvents;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Persistence.DbContexts;
using Order.Infrastructure.Persistence.Outbox;
using Order.Infrastructure.Repositories;
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

            serviceDescriptors.AddScoped<IOrderRepository, OrderRepository>();
            serviceDescriptors.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            serviceDescriptors.AddSingleton(_ => new JsonSerializerOptions());
            serviceDescriptors.AddScoped<IOutboxMessageFactory, SystemTextJsonOutboxMessageFactory>();
            serviceDescriptors.AddScoped<IIntegrationEventCollector, IntegrationEventCollector>();
            serviceDescriptors.AddScoped(typeof(DomainEventHandlerExecutor<>));
            serviceDescriptors.AddScoped<
                                        IDomainEventHandler<OrderCreatedDomainEvent>,
                                        OrderCreatedDomainEventHandler>();
            serviceDescriptors.AddScoped<IUnitOfWork, Order.Infrastructure.Persistence.EfUnitOfWork>();
            serviceDescriptors.AddHostedService<OutboxProcessorBackgroundService>();
            serviceDescriptors.AddSingleton<IIntegrationEventTypeRegistry, IntegrationEventTypeRegistry>();
            serviceDescriptors.AddScoped<IIntegrationEventCollector, IntegrationEventCollector>();

            return serviceDescriptors;
        }
    }
}
