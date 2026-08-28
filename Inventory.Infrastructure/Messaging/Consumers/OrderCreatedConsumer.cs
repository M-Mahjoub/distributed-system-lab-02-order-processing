using BuildingBlocks.Contracts.IntegrationEvents.Orders;
using Inventory.Application.Features.Inventory.Reserve;
using MediatR;

namespace Inventory.Infrastructure.Messaging.Consumers
{
    public class OrderCreatedConsumer
    {
        private readonly ISender _sender;

        public OrderCreatedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task ConsumeAsync(
            OrderCreatedIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            var items = message.Items
                               .Select(x =>
                                   new ReserveInventoryItem(
                                       x.ProductId,
                                       x.Quantity))
                               .ToList();


            var command = new ReserveInventoryCommand(
                message.OrderId,
                items);

            await _sender.Send(
                command,
                cancellationToken);
        }
    }
}
