using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Contracts.Inventory;
using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Persistence.Transactions;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Application.Messaging;
using Inventory.Infrastructure.Persistence.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureDI(this IServiceCollection service,
                                                             IConfigurationManager configurationManager)
        {
            service.AddScoped<IUnitOfWork, EfUnitOfWork<InventoryDbContext>>();

            service.AddScoped<
                ITransactionManager,
                EfTransactionManager<InventoryDbContext>>();

            service.AddScoped<IMessageHandler<ReleaseInventoryIntegrationCommand>, ReleaseInventoryMessageHandler>();

            return service;
        }
    }
}
