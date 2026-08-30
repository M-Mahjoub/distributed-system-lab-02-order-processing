using BuildingBlocks.Application.Messaging.Inbox;
using BuildingBlocks.Contracts.IntegrationEvents.Orders;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Application.Features.Inventory.Reserve;
using MediatR;

namespace Inventory.Application.Messaging
{
    public sealed class OrderCreatedMessageHandler
    {
        private readonly IInboxRepository _inbox;
        private readonly ISender _sender;
        private readonly ITransactionManager _transactionManager;

        public OrderCreatedMessageHandler(
            IInboxRepository inbox,
            ISender sender,
            ITransactionManager transactionManager)
        {
            _inbox = inbox;
            _sender = sender;
            _transactionManager = transactionManager;
        }

        public Task HandleAsync(
            OrderCreatedIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            return _transactionManager.ExecuteAsync(
                async ct =>
                {
                    if (await _inbox.ExistsAsync(
                            message.EventId,
                            nameof(OrderCreatedIntegrationEvent),
                            ct))
                    {
                        return;// Result.Success();
                    }

                    //await _inbox.AddAsync(
                    //    message.EventId,
                    //    nameof(OrderCreatedIntegrationEvent),
                    //    DateTime.UtcNow,
                    //    ct);

                    await _inbox.AddAsync(
                       new InboxMessage(message.EventId, nameof(OrderCreatedIntegrationEvent), DateTime.UtcNow),
                        ct);

                    var items = message.Items
                        .Select(x =>
                            new ReserveInventoryItem(
                                x.ProductId,
                                x.Quantity))
                        .ToList();

                    var command =
                        new ReserveInventoryCommand(
                            message.OrderId,
                            items);

                    var result =
                        await _sender.Send(command, ct);

                    if (!result.IsSuccess)
                        return;// result;

                    await _inbox.MarkProcessedAsync(
                        message.EventId,
                        DateTime.UtcNow,
                        ct);

                    return;// Result.Success();
                },
                cancellationToken);
        }
    }
}
