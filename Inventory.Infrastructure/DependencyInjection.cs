using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Messaging.Inbox;
using BuildingBlocks.Contracts.Inventory;
using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Persistence.Transactions;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Application.Messaging;
using Inventory.Infrastructure.Persistence.DbContexts;
using Inventory.Infrastructure.Persistence.Inbox;
using Inventory.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureDI(this IServiceCollection services,
                                                             IConfigurationManager configurationManager)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork<InventoryDbContext>>();

            services.AddScoped<
                ITransactionManager,
                EfTransactionManager<InventoryDbContext>>();

            services.AddScoped<
    IInboxRepository,
    InboxRepository>();

            services.AddScoped<
    IProductInventoryRepository,
    ProductInventoryRepository>();

            //services.AddScoped<
            //    IOutboxRepository,
            //    OutboxRepository>();

            services.AddScoped<
    IMessageHandler<ReleaseInventoryIntegrationCommand>,
    ReleaseInventoryMessageHandler>();

            services.AddScoped<
    ITransactionalMessageHandler<ReleaseInventoryIntegrationCommand>>(
    provider =>
    {
        var transactionManager =
            provider.GetRequiredService<ITransactionManager>();

        var inboxRepository =
            provider.GetRequiredService<IInboxRepository>();

        var handler =
            provider.GetRequiredService<
                IMessageHandler<ReleaseInventoryIntegrationCommand>>();

        var idAccessor =
           provider.GetRequiredService<
               IMessageIdAccessor<
                   ReleaseInventoryIntegrationCommand>>();

        return new TransactionalMessageHandler<
            ReleaseInventoryIntegrationCommand>(
            transactionManager,
            inboxRepository,
            handler,
            idAccessor,
            "Inventory.ReleaseInventoryConsumer");
    });

            services.AddScoped<IMessageHandler<ReleaseInventoryIntegrationCommand>, ReleaseInventoryMessageHandler>();

            return services;
        }
    }
}
